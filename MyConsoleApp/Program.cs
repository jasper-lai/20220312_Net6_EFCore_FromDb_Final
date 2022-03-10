// See https://aka.ms/new-console-template for more information

// ==============================
// namespaces 引用
// ==============================
using Microsoft.Extensions.Configuration;
using MyConsoleApp.Models;
using MyConsoleApp.Services;


// ==============================
// 非同步讀取 Db
// ==============================
var _service = new StudentGradeService();
var studentGrades = await _service.GetStudentGrades();

foreach (var item in studentGrades)
{
    Console.WriteLine($"thread: {Environment.CurrentManagedThreadId} --> {item.EnrollmentId} | {item.Grade} | {item.StudentName} | {item.CourseName} | {item.CourseCredits}");
}

// ==============================
// 讀取 appsettings.json 測試
// ==============================

// ----------------------
// ReadAppSettingsJson
// ----------------------
/// <summary>
/// 利用 Microsoft.Extensions.Configuration.* 讀取 appsettings.json
/// </summary>
/// <remarks>
/// 這個是 local function, 不可以有 private / public 的修飾字
/// </remarks>
void ReadAppSettingsJson()
{
    // 建立 config 物件, 採用 JSON 及 環境收數 提供者, 此時已將 appsettings.json 的內容讀入
    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

    // 由 config 物件, 取得已讀入至記憶體的內容, 並填入至 settings 變數 (MyAppSettings 煩別)
    MyAppSettings settings = config.GetRequiredSection("Settings").Get<MyAppSettings>();

    // 呈現結果值
    Console.WriteLine($"KeyOne = {settings.KeyOne}");
    Console.WriteLine($"KeyTwo = {settings.KeyTwo}");
    Console.WriteLine($"KeyThree:Message = {settings.KeyThree.Message}");

    // 由 config 物件, 取得已讀入至記憶體的內容, 並填入至 rests 變數 (NestedSettings 煩別)
    NestedSettings nests = config.GetRequiredSection("Settings:KeyThree").Get<NestedSettings>();

    // 呈現結果值
    Console.WriteLine($"nests:Message = {nests.Message}");
}

ReadAppSettingsJson();

