using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas_Wizard.Utils;
using Core;
using Microsoft.Win32;
using TrustedUninstaller.CLI;
using TrustedUninstaller.Shared;


namespace Atlas_Wizard.Utils
{
    public class DisableDefender
    {
        public static async Task<List<bool>> GetDefenderToggles()
        {
            var result = new List<bool>();

            await Task.Run(() =>
            {
                var defenderKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows Defender");
                var policiesKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender");

                RegistryKey realtimePolicy = null;
                RegistryKey realtimeKey = null;
                try
                {
                    try
                    {
                        realtimePolicy = policiesKey.OpenSubKey("Real-Time Protection");
                    }
                    catch (Exception e)
                    {
                    }

                    if (realtimePolicy != null)
                        realtimeKey = realtimePolicy;
                    else
                        realtimeKey = defenderKey.OpenSubKey("Real-Time Protection");
                }
                catch
                {
                    result.Add(false);
                }

                if (realtimeKey != null)
                {
                    try
                    {
                        result.Add((int)realtimeKey.GetValue("DisableRealtimeMonitoring") != 1);
                    }
                    catch (Exception exception)
                    {
                        try
                        {
                            realtimeKey = defenderKey.OpenSubKey("Real-Time Protection");
                            result.Add((int)realtimeKey.GetValue("DisableRealtimeMonitoring") != 1);
                        }
                        catch (Exception e)
                        {
                            result.Add(true);
                        }
                    }
                }

                try
                {
                    RegistryKey spynetPolicy = null;
                    RegistryKey spynetKey = null;

                    try
                    {
                        spynetPolicy = policiesKey.OpenSubKey("SpyNet");
                    }
                    catch (Exception e)
                    {
                    }

                    if (spynetPolicy != null)
                        spynetKey = spynetPolicy;
                    else
                        spynetKey = defenderKey.OpenSubKey("SpyNet");

                    int reporting = 0;
                    int consent = 0;
                    try
                    {
                        reporting = (int)spynetKey.GetValue("SpyNetReporting");
                    }
                    catch (Exception e)
                    {
                        if (spynetPolicy != null)
                        {
                            reporting = (int)defenderKey.OpenSubKey("SpyNet").GetValue("SpyNetReporting");
                        }
                    }

                    try
                    {
                        consent = (int)spynetKey.GetValue("SubmitSamplesConsent");
                    }
                    catch (Exception e)
                    {
                        if (spynetPolicy != null)
                        {
                            consent = (int)defenderKey.OpenSubKey("SpyNet").GetValue("SubmitSamplesConsent");
                        }
                    }

                    result.Add(reporting != 0);
                    result.Add(consent != 0 && consent != 2 && consent != 4);
                }
                catch
                {
                    result.Add(false);
                    result.Add(false);
                }

                try
                {
                    int tamper = (int)defenderKey.OpenSubKey("Features").GetValue("TamperProtection");
                    result.Add(tamper != 4 && tamper != 0);
                }
                catch
                {
                    result.Add(false);
                }
            });
            return result;
        }

        public async void CheckForDefender()
        {
            if (((AmeliorationUtil.Playbook.Requirements.Contains(Requirements.Requirement.DefenderDisabled) || AmeliorationUtil.Playbook.Requirements.Contains(Requirements.Requirement.DefenderToggled))))
            {
                bool first = true;

                while ((await GetDefenderToggles()).Any(x => x))
                {
                    
                }
            }
        }
        public void OpenDefender()
        {
            CommandPromptHelper.RunCommand("windowsdefender://");
        }
    }
}
