using PCShutdown.Classes.DarkMode;
using PCShutdown.Forms;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShutdown.Classes
{
    internal class FormsManager
    {
        
        public class Form
        {
            public string Name { get; private set; }
            public System.Windows.Forms.Form Instance { get; private set; }

            public Form(string name, System.Windows.Forms.Form instance)
            {
                Name = name;
                Instance = instance;
            }

            public void UpdateInstance(System.Windows.Forms.Form instance)
            {
                Instance = instance;
            }
        }

        

        private List<Form> Forms { get; set; }

        public FormsManager()
        {
            Forms = new List<Form>();
        }



        public List<Form> GetForms()
        {
            return Forms;
        }

        public List<string> GetFormsNames()
        {
            List<string> names = new();

            foreach (Form form in Forms) 
            {
                names.Add(form.Name);
            }

            return names;
        }

        

        public void ShowForm(Type FormType, params object[] args) 
        {
            
            Form find = Forms.Find(x => x.Name == FormType.Name);
            if (args.Length != 0)
            {
                if (find != null && !find.Instance.IsDisposed)
                    find.Instance.Dispose();
                Forms.Remove(find);
               
                find = null;
            }
            if ( find != null)
            {
                
                if(find.Instance.IsDisposed)
                {
                    var instance = (System.Windows.Forms.Form)Activator.CreateInstance(FormType);
                    find.UpdateInstance(instance);
                }
                ShutdownApp.GlobalForm.Invoke((MethodInvoker)delegate () {
                    var instance = find.Instance;
                    instance.StartPosition = FormStartPosition.CenterScreen;
                    if (Program.Cfg.DarkMode)
                    {
                        _ = new DarkModeCS(instance);
                    }
                    if (instance.InvokeRequired)
                    {
                        instance.Invoke((MethodInvoker)delegate () {
                            instance.Show();
                            instance.Activate();
                        });
                    }
                    else
                    {
                        instance.Show();
                        instance.Activate();
                    }
                    
                });
                
            }
            else
            {

                System.Windows.Forms.Form instance = null;
                if (args.Length > 0)
                    instance = (System.Windows.Forms.Form)Activator.CreateInstance(FormType, args);
                else
                    instance = (System.Windows.Forms.Form)Activator.CreateInstance(FormType);
                Forms.Add(new Form(FormType.Name, instance));
                _ = ShutdownApp.GlobalForm.Invoke((MethodInvoker)delegate ()
                {
                    instance.StartPosition = FormStartPosition.CenterScreen;
                    if (Program.Cfg.DarkMode)
                    {
                        _ = new DarkModeCS(instance);
                    }
                    if (instance.InvokeRequired)
                    {
                        instance.Invoke((MethodInvoker)delegate ()
                        {
                            instance.Show();
                            instance.Activate();
                        });
                    }
                    else
                    {
                        instance.Show();
                        instance.Activate();
                    }
                });
            }
        }

        public void CloseForm(Type FormType, params object[] args) 
        {
            Form find = Forms.Find(x => x.Name == FormType.Name);
            if (find != null)
            {
                if (!find.Instance.IsDisposed && find.Instance != null)
                {
                    ShutdownApp.GlobalForm.Invoke((MethodInvoker)delegate ()
                    {
                        if (find.Instance.GetType() == typeof(ScreenLockerForm))
                        {
                            ScreenLockerForm locker = (ScreenLockerForm)find.Instance;


                            if (locker.InvokeRequired)
                            {
                                locker.Invoke((MethodInvoker)delegate ()
                                {
                                    locker.Unlock(args[0].ToString());
                                });
                            }
                            else
                            {
                                locker.Unlock(args[0].ToString());
                            }
                        }

                        find.Instance.Close();
                    });
                }
            }

        }
    }
}
