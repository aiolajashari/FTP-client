using System;
using System.IO;
using System.Net;

namespace FTP
{
    class Program
    {
        static void Main(string[] args)
        {

            //Listim t'fajllav, shkarkim, riemrim, kopjim, fshierje
            string Pergjigje;

            Label: 

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nZgjedh njeren nga mundesite:");
            Console.WriteLine("1. Listim i fajllave; \n2. Shkarkim i fajllit; \n3. Riemrim i fajllit;\n4. Fshierja e fajllit; \n5. Mbyll konsolen. ");
            Console.Write("\nJu zgjedhni: ");
            int Shfrytezuesi = Int32.Parse(Console.ReadLine());

            switch (Shfrytezuesi)
            {
                case 1:
                    {
                        Console.WriteLine("\nLista e fajllave: ");
                        ListimMetode();
                    Pyetja:
                        Console.WriteLine("Deshiron te vazhdosh?: PO/JO");
                        Pergjigje = Console.ReadLine();

                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "po"))
                        {
                            goto Label;
                        }
                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "jo"))
                            break;
                        else
                        {
                            goto Pyetja;
                        } 
                    }

                case 2:
                    {
                        Console.WriteLine("\nShkarkimi i fajllave: ");
                        DownloadMetode();

                    Pyetja:
                        Console.WriteLine("Deshiron te vazhdosh?: PO/JO");
                        Pergjigje = Console.ReadLine();

                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "po"))
                        {
                            goto Label;
                        }
                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "jo"))
                            break;
                        else
                        {
                            goto Pyetja;
                        }
                    }

                case 3:
                    {
                        Console.WriteLine("\nRiemrimi i fajllave: ");
                        RenameMetode();

                    Pyetja:
                        Console.WriteLine("Deshiron te vazhdosh?: PO/JO");
                        Pergjigje = Console.ReadLine();

                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "po"))
                        {
                            goto Label;
                        }
                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "jo"))
                            break;
                        else
                        {
                            goto Pyetja;
                        }
                    }
                    

                case 4:
                    { 
                        Console.WriteLine("\nFshierja i fajllave: ");
                        DeleteMetode();

                    Pyetja:
                        Console.WriteLine("Deshiron te vazhdosh?: PO/JO");
                        Pergjigje = Console.ReadLine();

                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "po"))  
                        {
                            goto Label;
                        }
                        if (StringComparer.CurrentCultureIgnoreCase.Equals(Pergjigje, "jo"))
                            break;
                        else
                        {
                            goto Pyetja;
                        }
                    }

                case 5:
                    break;

                default:
                    goto Label;

            }
        }

        public static void ListimMetode()
        {

            Console.Write("Shkruaj local path prej nga deshironi te beni listimin: ");
            string PathListim = Console.ReadLine();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + PathListim); 
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            
            request.Credentials = new NetworkCredential("admin", "");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Listimi perfundoi me sukses, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();
        }

        public static void DownloadMetode()
        {
            Console.Write("Shkruaj local path prej nga deshironi te shkarkoni: ");
            string PathShkarkim = Console.ReadLine();
            Console.Write("Fajllin te cilin deshironi te shkarkoni: ");
            string fajlli = Console.ReadLine();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + PathShkarkim + "/" + fajlli);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("admin", "");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Shkarkimi perfundoi me sukses, statusi {0}", response.StatusDescription);

            reader.Close();
            response.Close();
        }


        public static void RenameMetode()
        {

            Console.Write("Shkruaj local path prej nga deshironi te shkarkoni: ");
            string PathRiemrimi = Console.ReadLine();
            Console.Write("Shkruani fajllin qe deshironi te riemroni:");
            string fajlli = Console.ReadLine();

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + PathRiemrimi + "/" + fajlli));
            request.Proxy = null;
            request.Credentials = new NetworkCredential("admin", "");

            Console.Write("Shkruani emrimin e ri te fajllit: ");
            string emriFajllit = Console.ReadLine();

            string EmriIRi = fajlli.Replace(fajlli, emriFajllit);
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = EmriIRi;
            request.GetResponse();


            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("\nRiemrimi perfundoi me sukses, statusi {0}", response.StatusDescription);
        }
        

        public static void DeleteMetode()
        {
            Console.Write("Shkruaj local path ku deshironi te fshijeni: ");
            string PathDelete = Console.ReadLine();
            Console.Write("Shkruaj emrin e fajllit qe deshironi ta fshijeni: ");
            string Fajlli = Console.ReadLine();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + PathDelete + "/" + Fajlli);
            request.Credentials = new NetworkCredential("admin", "");
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("\nDelete Complete, status {0}", response.StatusDescription);
        }
    }
}