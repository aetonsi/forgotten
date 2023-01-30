using System.Diagnostics;

Func<string, string[], Process> invisible_process = (program, args) =>
{
    ProcessStartInfo psi = new ProcessStartInfo
    {
        FileName = program,
        WorkingDirectory = Directory.GetCurrentDirectory(),
        UseShellExecute = false,
        CreateNoWindow = true,
        WindowStyle = ProcessWindowStyle.Hidden
    };
    foreach (string arg in args) psi.ArgumentList.Add(arg);
    Process p = new Process();
    p.StartInfo = psi;
    return p;
};


string[] invocationArguments = Environment.GetCommandLineArgs().Skip(1).ToArray();

string invisible_proxy = "invisible";
string[] program_to_run = invocationArguments.ToArray();

Process p = invisible_process(invisible_proxy, program_to_run);
p.Start();
p.WaitForExit();

Environment.Exit(p.HasExited ? p.ExitCode : 0);
