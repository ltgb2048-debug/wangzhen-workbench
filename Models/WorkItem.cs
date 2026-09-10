using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WangzhenWorkbench.Models;

public sealed class WorkItem : INotifyPropertyChanged
{
    private int _progress;
    private bool _isCompleted;

    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = "日常";
    public DateTime DueAt { get; set; }
    public string Priority { get; set; } = "B";
    public bool IsManualProgress { get; set; }
    public bool IsMilestone { get; set; }
    public bool Notified { get; set; }

    public int Progress
    {
        get => _progress;
        set
        {
            var next = Math.Clamp(value, 0, 100);
            if (_progress == next) return;
            _progress = next;
            if (_progress >= 100 && !_isCompleted)
            {
                _isCompleted = true;
                OnPropertyChanged(nameof(IsCompleted));
            }
            else if (_progress < 100 && _isCompleted)
            {
                _isCompleted = false;
                OnPropertyChanged(nameof(IsCompleted));
            }
            OnPropertyChanged();
            OnPropertyChanged(nameof(StatusText));
        }
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (_isCompleted == value) return;
            _isCompleted = value;
            if (!_isManualProgress)
            {
                _progress = value ? 100 : 0;
                OnPropertyChanged(nameof(Progress));
            }
            OnPropertyChanged();
            OnPropertyChanged(nameof(StatusText));
        }
    }

    public string DueText => IsMilestone
        ? DueAt.ToString("MM月dd日 HH:mm")
        : DueAt.ToString("HH:mm");

    public string StatusText
    {
        get
        {
            if (IsCompleted) return "已完成";
            if (DateTime.Now > DueAt) return "已逾期";
            var span = DueAt - DateTime.Now;
            if (span.TotalDays >= 1) return $"剩 {Math.Ceiling(span.TotalDays)} 天";
            if (span.TotalHours >= 1) return $"剩 {Math.Ceiling(span.TotalHours)} 小时";
            return $"剩 {Math.Max(0, Math.Ceiling(span.TotalMinutes))} 分钟";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
