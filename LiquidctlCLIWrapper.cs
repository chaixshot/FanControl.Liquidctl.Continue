using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using FanControl.Plugins;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading;


namespace FanControl.Liquidctl
{
    internal static class LiquidctlCLIWrapper
    {
         public static string liquidctlexe = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "liquidctl.exe");   // This should always resolve to the same directory as the FanControl.Liquidctl.dll
        //public static string liquidctlexe = "liquidctl";   // This should always resolve to the same directory as the FanControl.Liquidctl.dll
                                                           // TODO: extract path to executable to config(?) - Seems to work fine now though
        internal static IPluginLogger logger;

        internal static void Initialize(IPluginLogger pluginLogger)
        {
            logger = pluginLogger;

            LiquidctlCall($"--json initialize all");
        }

        internal static List<LiquidctlStatusJSON> ReadStatus()
        {
            string outout = LiquidctlCall($"--json status");
            return ParseStatuses(outout);
        }

        internal static List<LiquidctlStatusJSON> ReadStatus(string address)
        {
            string outout = LiquidctlCall($"--json --address \"{address}\" status");
            return ParseStatuses(outout);
        }

        internal static void SetPump(string address, int value)
        {
            LiquidctlCall($"--address \"{address}\" set pump speed \"{(value)}\"");
        }

        internal static void SetFan(string address, int value)
        {
            LiquidctlCall($"--address \"{address}\" set fan speed \"{(value)}\"");
        }

        internal static void SetMicroFan(string address, int value)
        {
            LiquidctlCall($"--address \"{address}\" set pump-fan speed \"{(value)}\"");
        }

        internal static void SetFanNumber(string address, int index, int value)
        {
            LiquidctlCall($"--address \"{address}\" set fan{index} speed \"{(value)}\"");
        }

        private static string LiquidctlCall(string arguments)
        {
            string output = "";
            for (int retry = 0; retry < 10; retry++)
            {

                Process process = new Process();

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.StartInfo.FileName = liquidctlexe;
                process.StartInfo.Arguments = arguments;

                process.Start();
                process.WaitForExit();

                output = process.StandardOutput.ReadToEnd().ToString();

                if (process.ExitCode == 0)
                {
                    break;
                }
                else if (retry >= 5)
                {
                    throw new Exception($"-------------------------------\n" +
                        $"liquidctl returned non-zero exit code {process.ExitCode}.\n" +
                        $"Arguments:\nliquidctl {arguments}\n" +
                        $"Last stderr output:\n{process.StandardError.ReadToEnd()}" +
                        $"-------------------------------");
                }
                Thread.Sleep(1500);
                logger.Log($"Retrying ({retry}):\nliquidctl {arguments}" );
            }
            return output;
        }

        // Code by akotulu
        // See https://github.com/jmarucha/FanControl.Liquidctl/pull/29/commits/145978bdf1c2d1a464b2a036b4fc26f559bb77dc#diff-d7a2c0cf4c270870ed263c55d2cd4fc41258347085a3cded3a78b48e73f78092
        private static List<LiquidctlStatusJSON> ParseStatuses(string json)
        {
            JArray statusArray = JArray.Parse(json);
            List<LiquidctlStatusJSON> statuses = new List<LiquidctlStatusJSON>();

            foreach (JObject statusObject in statusArray)
            {
                try
                {
                    LiquidctlStatusJSON status = statusObject.ToObject<LiquidctlStatusJSON>();
                    statuses.Add(status);
                }
                catch (Exception e)
                {
                    logger.Log($"Unable to parse {statusObject}\n{e.Message}");
                }
            }

            return statuses;
        }
    }
}
