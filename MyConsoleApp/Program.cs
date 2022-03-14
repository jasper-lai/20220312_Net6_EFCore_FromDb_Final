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

// ==============================
// Null Forgiving Operator 
// ==============================

void NullForgivingTest1()
{
    string x;
    string? y = null;

    //Warning: CS8600 "Converting null literal or possible null value to non-nullable type"
    x = y;   
    Console.WriteLine( ( x is null ) ? "x is null" : "x is not null");

    //雖然 string x 在語意上, 是不允許 null 的; 但仍可將 null! 或變數 y! 指定給 x, 讓編譯器在作靜態分析時, 不會顯示謷告訊息.
    //[重要] ! 這個運算子, 只是關掉編譯器在 型別系統檢查時的警告, 在執行階段, 其內容值仍然可以是 null.
    x = y!;  
    Console.WriteLine( (x is null) ? "x is null" : "x is not null");

    //上述 2 個 Console.WriteLine, 均會顯示 x is null
}


void NullForgivingTest2()
{
    var obj0 = new AppInfo();
    var obj1 = new NullFix1_AppInfo();
    var obj2 = new NullFix2_AppInfo();
    var obj3 = new NullFix3_AppInfo();
    var obj4 = new NullFix4_AppInfo();

    Console.WriteLine((obj0.Name is null) ? "obj0.Name is null" : "obj0.Name is not null");
    Console.WriteLine((obj1.Name is null) ? "obj1.Name is null" : "obj1.Name is not null");
    Console.WriteLine((obj2.Name is null) ? "obj2.Name is null" : "obj2.Name is not null");
    Console.WriteLine((obj3.Name is null) ? "obj3.Name is null" : "obj3.Name is not null");
    Console.WriteLine((obj4.Name is null) ? "obj4.Name is null" : "obj4.Name is not null");

    //上述 Console.WriteLine, 只有第4個會顯示 "obj4.Name is not null".
}

NullForgivingTest1();
NullForgivingTest2();

