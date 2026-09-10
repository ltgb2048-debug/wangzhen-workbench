using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WangzhenWorkbench.Data;
using WangzhenWorkbench.Models;
using WangzhenWorkbench.Services;
using Forms = System.Windows.Forms;

namespace WangzhenWorkbench;

public partial class MainWindow : System.Windows.Window
{
    private readonly WorkbenchDatabase _db = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(30) };
    private readonly Forms.NotifyIcon _notifyIcon;
    private bool _allowClose;
    private DateTime _loadedDate = DateTime.Today;

    public ObservableCollection<WorkItem> TodayTasks { get; } = new();
    public ObservableCollection<WorkItem> Milestones { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        _db.Initialize();
        LoadData();
        DateText.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd", new CultureInfo("zh-CN"));

        _notifyIcon = new Forms.NotifyIcon
        {
            Icon = System.Drawing.SystemIcons.Application,
            Text = "王震工作进度台",
            Visible = true
        };
        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add("显示工作台", null, (_, _) => Dispatcher.Invoke(ShowFromTray));
        menu.Items.Add("退出", null, (_, _) => Dispatcher.Invoke(ExitApplication));
        _notifyIcon.ContextMenuStrip = menu;
        _notifyIcon.DoubleClick += (_, _) => Dispatcher.Invoke(ShowFromTray);

        _timer.Tick += Timer_Tick;
        _timer.Start();

        Closing += MainWindow_Closing;
        StateChanged += MainWindow_StateChanged;
        UpdateStartupButton();
        CheckReminders();
    }

    private void LoadData()
    {
        foreach (var item in TodayTasks) item.PropertyChanged -= Task_PropertyChanged;
        TodayTasks.Clear();
        foreach (var item in _db.GetTodayTasks())
        {
            item.PropertyChanged += Task_PropertyChanged;
            TodayTasks.Add(item);
        }

        Milestones.Clear();
        foreach (var item in _db.GetUpcomingMilestones()) Milestones.Add(item);
        UpdateOverallProgress();
    }

    private void Task_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not WorkItem item) return;
        if (e.PropertyName is nameof(WorkItem.Progress) or nameof(WorkItem.IsCompleted))
        {
            _db.UpdateTask(item);
            UpdateOverallProgress();
        }
    }

    private void UpdateOverallProgress()
    {
        var value = TodayTasks.Count == 0 ? 0 : (int)Math.Round(TodayTasks.Average(x => x.Progress));
        OverallProgress.Value = value;
        OverallText.Text = $"{value}%";
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_loadedDate != DateTime.Today)
        {
            _loadedDate = DateTime.Today;
            _db.Initialize();
            LoadData();
            DateText.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd", new CultureInfo("zh-CN"));
        }

        CheckReminders();
        TodayTasksViewRefresh();
    }

    private void TodayTasksViewRefresh()
    {
        System.Windows.Data.CollectionViewSource.GetDefaultView(TodayTasks).Refresh();
        System.Windows.Data.CollectionViewSource.GetDefaultView(Milestones).Refresh();
    }

    private void CheckReminders()
    {
        foreach (var item in _db.GetDueForReminder(DateTime.Now))
        {
            _notifyIcon.ShowBalloonTip(10000, "工作进度提醒", $"{item.Title} 到时间了。当前进度 {item.Progress}%", Forms.ToolTipIcon.Info);
            SystemSounds.Asterisk.Play();
            _db.MarkNotified(item.Id);
        }
    }

    private void AddTask_Click(object sender, RoutedEventArgs e)
    {
        var title = NewTaskTitleBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            StatusText.Text = "先输入任务名称。";
            return;
        }

        if (!TimeSpan.TryParseExact(DueTimeBox.Text.Trim(), new[] { "h\\:mm", "hh\\:mm" }, CultureInfo.InvariantCulture, out var time))
        {
            StatusText.Text = "截止时间格式请填写 17:30 这类格式。";
            return;
        }

        var priority = (PriorityBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "A";
        _db.AddTask(title, DateTime.Today.Add(time), priority, ManualProgressBox.IsChecked == true);
        NewTaskTitleBox.Clear();
        LoadData();
        StatusText.Text = $"已添加：{title}";
    }

    private void IncreaseProgress_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as System.Windows.Controls.Button)?.Tag is not WorkItem item) return;
        item.Progress += 10;
    }

    private void DecreaseProgress_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as System.Windows.Controls.Button)?.Tag is not WorkItem item) return;
        item.Progress -= 10;
    }

    private void GenerateReport_Click(object sender, RoutedEventArgs e)
    {
        var finished = TodayTasks.Where(x => x.IsCompleted).ToList();
        var moving = TodayTasks.Where(x => !x.IsCompleted && x.Progress > 0).ToList();
        var pending = TodayTasks.Where(x => !x.IsCompleted && x.Progress == 0).ToList();

        var sb = new StringBuilder();
        sb.AppendLine($"{DateTime.Today:M月d日}工作简报：");
        sb.AppendLine();
        sb.AppendLine("一、今日完成");
        if (finished.Count == 0) sb.AppendLine("- 暂无已勾选完成事项");
        else foreach (var item in finished) sb.AppendLine($"- {item.Title}");

        sb.AppendLine();
        sb.AppendLine("二、正在推进");
        if (moving.Count == 0) sb.AppendLine("- 暂无手动记录的推进事项");
        else foreach (var item in moving) sb.AppendLine($"- {item.Title}：{item.Progress}%");

        sb.AppendLine();
        sb.AppendLine("三、下一步重点");
        var next = pending.OrderBy(x => x.DueAt).Take(3).ToList();
        if (next.Count == 0) sb.AppendLine("- 今日任务已清完，明日根据项目节点继续推进");
        else foreach (var item in next) sb.AppendLine($"- {item.Title}");

        var milestone = Milestones.FirstOrDefault();
        if (milestone is not null)
        {
            sb.AppendLine();
            sb.AppendLine($"最近节点：{milestone.Title}（{milestone.DueAt:MM月dd日 HH:mm}）");
        }

        System.Windows.Clipboard.SetText(sb.ToString());
        StatusText.Text = "今日汇报已复制到剪贴板，可直接粘贴后微调。";
    }

    private void DesktopMode_Click(object sender, RoutedEventArgs e)
    {
        Topmost = false;
        ShowInTaskbar = false;
        Left = Math.Max(SystemParameters.WorkArea.Left, SystemParameters.WorkArea.Right - ActualWidth - 18);
        Top = SystemParameters.WorkArea.Top + 18;
        StatusText.Text = "已切换桌面模式：不抢前台、不占任务栏。";
    }

    private void FloatMode_Click(object sender, RoutedEventArgs e)
    {
        ShowInTaskbar = true;
        Topmost = true;
        Activate();
        StatusText.Text = "已切换悬浮模式：窗口保持最前。";
    }

    private void Startup_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var enabled = StartupService.Toggle();
            UpdateStartupButton();
            StatusText.Text = enabled ? "已开启开机自启。" : "已关闭开机自启。";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"开机自启设置失败：{ex.Message}";
        }
    }

    private void UpdateStartupButton()
    {
        StartupButton.Content = StartupService.IsEnabled() ? "关闭开机自启" : "开启开机自启";
    }

    private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed) DragMove();
    }

    private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void Close_Click(object sender, RoutedEventArgs e) => Close();

    private void MainWindow_StateChanged(object? sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            Hide();
            _notifyIcon.ShowBalloonTip(3000, "王震工作进度台", "工作台仍在后台提醒，双击托盘图标可恢复。", Forms.ToolTipIcon.Info);
        }
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        if (_allowClose) return;
        e.Cancel = true;
        Hide();
        StatusText.Text = "已隐藏到系统托盘。";
    }

    private void ShowFromTray()
    {
        Show();
        WindowState = WindowState.Normal;
        Activate();
    }

    private void ExitApplication()
    {
        _allowClose = true;
        _timer.Stop();
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        Close();
    }
}
