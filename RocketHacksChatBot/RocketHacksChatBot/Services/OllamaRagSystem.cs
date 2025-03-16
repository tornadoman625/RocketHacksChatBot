
using LangChain.Databases.Sqlite;
using LangChain.DocumentLoaders;
using LangChain.Providers.Ollama;
using LangChain.Extensions;
using System.Diagnostics;
using System.Text;
namespace RocketHacksChatBot.Services;
public class OllamaRAGSystem
{

    public async Task<string> getContext(string arguments)
    {
        try
        {

            string baseDirectory = Environment.CurrentDirectory;

            // Combine the base directory with the script folder and script name.
            string scriptPath = baseDirectory+ "\\PythonScripts"+ "\\get_context.py";
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "python3", // Or "python3"
                Arguments = $"{scriptPath} {arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(baseDirectory + "\\PythonScripts\\vectorDb"),
                StandardOutputEncoding = Encoding.UTF8

            };

            using (Process process = new Process { StartInfo = start })
            {
                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Python Error: {error}");
                    //throw new Exception($"Python script error: {error}");
                }

                return output;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running Python script: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }

}

