using Microsoft.Data.Sqlite;
using WangzhenWorkbench.Models;

namespace WangzhenWorkbench.Data;

public sealed class WorkbenchDatabase
{
    private readonly string _connectionString;

    public WorkbenchDatabase()
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WangzhenWorkbench");
        Directory.CreateDirectory(dir);
        var dbPath = Path.Combine(dir, "workbench.db");
        _connectionString = $"Data Source={dbPath}";
    }

    public void Initialize()
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE IF NOT EXISTS Tasks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Category TEXT NOT NULL,
                DueAt TEXT NOT NULL,
                Priority TEXT NOT NULL,
                Progress INTEGER NOT NULL DEFAULT 0,
                IsManualProgress INTEGER NOT NULL DEFAULT 0,
                IsCompleted INTEGER NOT NULL DEFAULT 0,
                IsMilestone INTEGER NOT NULL DEFAULT 0,
                Notified INTEGER NOT NULL DEFAULT 0,
                SeedKey TEXT NULL,
                CreatedAt TEXT NOT NULL
            );
            CREATE UNIQUE INDEX IF NOT EXISTS IX_Tasks_SeedKey ON Tasks(SeedKey) WHERE SeedKey IS NOT NULL;
            """;
        cmd.ExecuteNonQuery();
        SeedDefaults(conn);
    }

    private SqliteConnection Open()
    {
        var conn = new SqliteConnection(_connectionString);
        conn.Open();
        return conn;
    }

    private static void SeedDefaults(SqliteConnection conn)
    {
        var today = DateTime.Today;
        var seeds = new[]
        {
            new Seed("daily-sales-" + today.ToString("yyyyMMdd"), "昨日销售日报", "固定工作", today.AddHours(9).AddMinutes(40), "A", false, false),
            new Seed("daily-douyin-" + today.ToString("yyyyMMdd"), "抖音日更3条", "固定工作", today.AddHours(17).AddMinutes(50), "B", false, false),
            new Seed("daily-redbook-" + today.ToString("yyyyMMdd"), "小红书日更3条", "固定工作", today.AddHours(17).AddMinutes(50), "B", false, false),
            new Seed("daily-summary-" + today.ToString("yyyyMMdd"), "下班前记录今日完成", "固定工作", today.AddHours(18).AddMinutes(10), "A", false, false),

            // 2026-09-10 当前重点：明天事业单位专场前的收口任务。
            new Seed("work-20260910-live-sop", "事业单位直播SOP定稿", "事业单位专场", new DateTime(2026, 9, 10, 17, 0, 0), "S", true, false),
            new Seed("work-20260910-filter-test", "筛岗工具实战测试", "事业单位专场", new DateTime(2026, 9, 10, 17, 30, 0), "S", true, false),
            new Seed("work-20260910-material-check", "明日直播人员/物料确认", "事业单位专场", new DateTime(2026, 9, 10, 18, 0, 0), "A", false, false),

            new Seed("milestone-shiye", "事业单位日不落直播", "项目节点", new DateTime(2026, 9, 11, 10, 0, 0), "S", true, true),
            new Seed("milestone-skill", "Skill大赛截止", "项目节点", new DateTime(2026, 9, 15, 18, 0, 0), "S", true, true),
            new Seed("milestone-midautumn", "中秋专场启动", "项目节点", new DateTime(2026, 9, 24, 10, 0, 0), "A", true, true)
        };

        foreach (var item in seeds)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                INSERT OR IGNORE INTO Tasks
                (Title, Category, DueAt, Priority, Progress, IsManualProgress, IsCompleted, IsMilestone, Notified, SeedKey, CreatedAt)
                VALUES ($title, $category, $dueAt, $priority, 0, $manual, 0, $milestone, 0, $seedKey, $createdAt);
                """;
            cmd.Parameters.AddWithValue("$title", item.Title);
            cmd.Parameters.AddWithValue("$category", item.Category);
            cmd.Parameters.AddWithValue("$dueAt", item.DueAt.ToString("O"));
            cmd.Parameters.AddWithValue("$priority", item.Priority);
            cmd.Parameters.AddWithValue("$manual", item.Manual ? 1 : 0);
            cmd.Parameters.AddWithValue("$milestone", item.Milestone ? 1 : 0);
            cmd.Parameters.AddWithValue("$seedKey", item.SeedKey);
            cmd.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("O"));
            cmd.ExecuteNonQuery();
        }
    }

    public List<WorkItem> GetTodayTasks()
    {
        var start = DateTime.Today;
        var end = start.AddDays(1);
        return Query("IsMilestone = 0 AND DueAt >= $start AND DueAt < $end", (cmd) =>
        {
            cmd.Parameters.AddWithValue("$start", start.ToString("O"));
            cmd.Parameters.AddWithValue("$end", end.ToString("O"));
        });
    }

    public List<WorkItem> GetUpcomingMilestones()
    {
        return Query("IsMilestone = 1 AND IsCompleted = 0", _ => { }, "DueAt ASC");
    }

    public List<WorkItem> GetDueForReminder(DateTime now)
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT Id, Title, Category, DueAt, Priority, Progress, IsManualProgress, IsCompleted, IsMilestone, Notified
            FROM Tasks
            WHERE IsCompleted = 0 AND Notified = 0 AND DueAt <= $now
            ORDER BY DueAt ASC;
            """;
        cmd.Parameters.AddWithValue("$now", now.ToString("O"));
        return ReadItems(cmd);
    }

    private List<WorkItem> Query(
        string where,
        Action<SqliteCommand> bind,
        string orderBy = "CASE Priority WHEN 'S' THEN 0 WHEN 'A' THEN 1 ELSE 2 END, DueAt ASC")
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $"""
            SELECT Id, Title, Category, DueAt, Priority, Progress, IsManualProgress, IsCompleted, IsMilestone, Notified
            FROM Tasks
            WHERE {where}
            ORDER BY {orderBy};
            """;
        bind(cmd);
        return ReadItems(cmd);
    }

    private static List<WorkItem> ReadItems(SqliteCommand cmd)
    {
        var list = new List<WorkItem>();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new WorkItem
            {
                Id = reader.GetInt64(0),
                Title = reader.GetString(1),
                Category = reader.GetString(2),
                DueAt = DateTime.Parse(reader.GetString(3), null, System.Globalization.DateTimeStyles.RoundtripKind),
                Priority = reader.GetString(4),
                Progress = reader.GetInt32(5),
                IsManualProgress = reader.GetInt32(6) == 1,
                IsCompleted = reader.GetInt32(7) == 1,
                IsMilestone = reader.GetInt32(8) == 1,
                Notified = reader.GetInt32(9) == 1
            });
        }
        return list;
    }

    public long AddTask(string title, DateTime dueAt, string priority, bool manualProgress)
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            INSERT INTO Tasks
            (Title, Category, DueAt, Priority, Progress, IsManualProgress, IsCompleted, IsMilestone, Notified, CreatedAt)
            VALUES ($title, '临时任务', $dueAt, $priority, 0, $manual, 0, 0, 0, $createdAt);
            SELECT last_insert_rowid();
            """;
        cmd.Parameters.AddWithValue("$title", title);
        cmd.Parameters.AddWithValue("$dueAt", dueAt.ToString("O"));
        cmd.Parameters.AddWithValue("$priority", priority);
        cmd.Parameters.AddWithValue("$manual", manualProgress ? 1 : 0);
        cmd.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("O"));
        return (long)(cmd.ExecuteScalar() ?? 0L);
    }

    public void UpdateTask(WorkItem item)
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            UPDATE Tasks
            SET Progress = $progress,
                IsCompleted = $completed,
                Notified = CASE WHEN $completed = 1 THEN Notified ELSE 0 END
            WHERE Id = $id;
            """;
        cmd.Parameters.AddWithValue("$progress", item.Progress);
        cmd.Parameters.AddWithValue("$completed", item.IsCompleted ? 1 : 0);
        cmd.Parameters.AddWithValue("$id", item.Id);
        cmd.ExecuteNonQuery();
    }

    public void MarkNotified(long id)
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Tasks SET Notified = 1 WHERE Id = $id;";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.ExecuteNonQuery();
    }

    private sealed record Seed(
        string SeedKey,
        string Title,
        string Category,
        DateTime DueAt,
        string Priority,
        bool Manual,
        bool Milestone);
}
