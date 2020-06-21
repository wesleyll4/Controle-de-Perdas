using Controle_de_Perdas.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using System.Timers;

namespace Controle_de_Perdas
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        string UltimoMPR = null;
        string path = @"D:\kme\pt200\ProductReport\CM602-1";
        string SourcePath = @"D:\kme\\pt200\\ProductReport\CM602-1\0000001" + ".mpr";
        System.Timers.Timer atimer;


        public MainWindow()
            {
            InitializeComponent();
            }

        void Split_MPR(string SourcePath, string TargetPath)
            {
            try
                {
                var ListaDeComponentes = new List<Componente>();

                string lines = File.ReadAllText(SourcePath);

                char[] delimiterChars = { '[', ']' };
                string[] words = lines.Split(delimiterChars);


                string contagem = words[1] + words[2];
                string[] _contagem = contagem.Split("M3000");

                string PickupError = words[3] + words[4];
                string[] _pickUpError = PickupError.Split("M3000");

                string RecogError = words[5] + words[6];
                string[] _recogError = RecogError.Split("M3000");


                string Count = null;

                for (int i = 1; i < _contagem.Length; i++)
                    {
                    Count += _contagem[i];
                    }

                string[] Componentes = Count.Split(' ');


                int numeroDeComponentes = Componentes.Length / 60;



                Componente[] com = new Componente[_contagem.Length];

                for (int i = 1; i < _contagem.Length; i++)
                    {
                    if (com[i] == null)
                        {

                        com[i] = new Componente();

                        com[i].Maquina = "1";
                        com[i].Addres = _contagem[i].Split()[0];
                        com[i].SubAdr = _contagem[i].Split()[1];
                        com[i].PN = _contagem[i].Split()[3];
                        com[i].NPA += int.Parse(_contagem[i].Split()[4]);
                        com[i].NPB += int.Parse(_contagem[i].Split()[5]);
                        com[i].NPC += int.Parse(_contagem[i].Split()[6]);
                        com[i].NPD += int.Parse(_contagem[i].Split()[7]);
                        com[i].NPE += int.Parse(_contagem[i].Split()[8]);
                        com[i].NPF += int.Parse(_contagem[i].Split()[9]);
                        com[i].NPG += int.Parse(_contagem[i].Split()[10]);
                        com[i].NPH += int.Parse(_contagem[i].Split()[11]);
                        com[i].NPI += int.Parse(_contagem[i].Split()[12]);
                        com[i].NPJ += int.Parse(_contagem[i].Split()[13]);
                        com[i].NPK += int.Parse(_contagem[i].Split()[14]);
                        com[i].NPL += int.Parse(_contagem[i].Split()[15]);


                        com[i].PNPA += int.Parse(_pickUpError[i].Split()[4]);
                        com[i].PNPB += int.Parse(_pickUpError[i].Split()[5]);
                        com[i].PNPC += int.Parse(_pickUpError[i].Split()[6]);
                        com[i].PNPD += int.Parse(_pickUpError[i].Split()[7]);
                        com[i].PNPE += int.Parse(_pickUpError[i].Split()[8]);
                        com[i].PNPF += int.Parse(_pickUpError[i].Split()[9]);
                        com[i].PNPG += int.Parse(_pickUpError[i].Split()[10]);
                        com[i].PNPH += int.Parse(_pickUpError[i].Split()[11]);
                        com[i].PNPI += int.Parse(_pickUpError[i].Split()[12]);
                        com[i].PNPJ += int.Parse(_pickUpError[i].Split()[13]);
                        com[i].PNPK += int.Parse(_pickUpError[i].Split()[14]);
                        com[i].PNPL += int.Parse(_pickUpError[i].Split()[15]);


                        com[i].RNPA += int.Parse(_recogError[i].Split()[4]);
                        com[i].RNPB += int.Parse(_recogError[i].Split()[5]);
                        com[i].RNPC += int.Parse(_recogError[i].Split()[6]);
                        com[i].RNPD += int.Parse(_recogError[i].Split()[7]);
                        com[i].RNPE += int.Parse(_recogError[i].Split()[8]);
                        com[i].RNPF += int.Parse(_recogError[i].Split()[9]);
                        com[i].RNPG += int.Parse(_recogError[i].Split()[10]);
                        com[i].RNPH += int.Parse(_recogError[i].Split()[11]);
                        com[i].RNPI += int.Parse(_recogError[i].Split()[12]);
                        com[i].RNPJ += int.Parse(_recogError[i].Split()[13]);
                        com[i].RNPK += int.Parse(_recogError[i].Split()[14]);
                        com[i].RNPL += int.Parse(_recogError[i].Split()[15]);


                        com[i].TotalMontado += com[i].NPA + com[i].NPB + com[i].NPC + com[i].NPD + com[i].NPE + com[i].NPF + com[i].NPG + com[i].NPH + com[i].NPI + com[i].NPJ + com[i].NPK + com[i].NPL;
                        com[i].TotalPickupError += com[i].PNPA + com[i].PNPB + com[i].PNPC + com[i].PNPD + com[i].PNPE + com[i].PNPF + com[i].PNPG + com[i].PNPH + com[i].PNPI + com[i].PNPJ + com[i].PNPK + com[i].PNPL;
                        com[i].TotalRecogError += com[i].RNPA + com[i].RNPB + com[i].RNPC + com[i].RNPD + com[i].RNPE + com[i].RNPF + com[i].RNPG + com[i].RNPH + com[i].RNPI + com[i].RNPJ + com[i].RNPK + com[i].RNPL;

                        com[i].TotalPerdido += com[i]._TotalPerdido();
                        com[i].Porcentagem = com[i]._porcentagem().ToString("f2", CultureInfo.InvariantCulture) + " " + "%";


                        if (com[i]._porcentagem() >= 1.0)
                            {
                            com[i].State = "State1";
                            }
                        else
                            {
                            com[i].State = "State2";
                            }



                        if (com[i].SubAdr == "1")
                            {
                            com[i].Endereço = com[i].Addres + " " + "L";
                            }
                        else
                            {
                            com[i].Endereço = com[i].Addres + " " + "R";
                            }


                        }
                    else
                        {
                        com[i].Addres = _contagem[i].Split()[0];
                        com[i].SubAdr = _contagem[i].Split()[1];
                        com[i].PN = _contagem[i].Split()[3];
                        com[i].NPA += int.Parse(_contagem[i].Split()[4]);
                        com[i].NPB += int.Parse(_contagem[i].Split()[5]);
                        com[i].NPC += int.Parse(_contagem[i].Split()[6]);
                        com[i].NPD += int.Parse(_contagem[i].Split()[7]);
                        com[i].NPE += int.Parse(_contagem[i].Split()[8]);
                        com[i].NPF += int.Parse(_contagem[i].Split()[9]);
                        com[i].NPG += int.Parse(_contagem[i].Split()[10]);
                        com[i].NPH += int.Parse(_contagem[i].Split()[11]);
                        com[i].NPI += int.Parse(_contagem[i].Split()[12]);
                        com[i].NPJ += int.Parse(_contagem[i].Split()[13]);
                        com[i].NPK += int.Parse(_contagem[i].Split()[14]);
                        com[i].NPL += int.Parse(_contagem[i].Split()[15]);


                        com[i].PNPA += int.Parse(_pickUpError[i].Split()[4]);
                        com[i].PNPB += int.Parse(_pickUpError[i].Split()[5]);
                        com[i].PNPC += int.Parse(_pickUpError[i].Split()[6]);
                        com[i].PNPD += int.Parse(_pickUpError[i].Split()[7]);
                        com[i].PNPE += int.Parse(_pickUpError[i].Split()[8]);
                        com[i].PNPF += int.Parse(_pickUpError[i].Split()[9]);
                        com[i].PNPG += int.Parse(_pickUpError[i].Split()[10]);
                        com[i].PNPH += int.Parse(_pickUpError[i].Split()[11]);
                        com[i].PNPI += int.Parse(_pickUpError[i].Split()[12]);
                        com[i].PNPJ += int.Parse(_pickUpError[i].Split()[13]);
                        com[i].PNPK += int.Parse(_pickUpError[i].Split()[14]);
                        com[i].PNPL += int.Parse(_pickUpError[i].Split()[15]);


                        com[i].RNPA += int.Parse(_recogError[i].Split()[4]);
                        com[i].RNPB += int.Parse(_recogError[i].Split()[5]);
                        com[i].RNPC += int.Parse(_recogError[i].Split()[6]);
                        com[i].RNPD += int.Parse(_recogError[i].Split()[7]);
                        com[i].RNPE += int.Parse(_recogError[i].Split()[8]);
                        com[i].RNPF += int.Parse(_recogError[i].Split()[9]);
                        com[i].RNPG += int.Parse(_recogError[i].Split()[10]);
                        com[i].RNPH += int.Parse(_recogError[i].Split()[11]);
                        com[i].RNPI += int.Parse(_recogError[i].Split()[12]);
                        com[i].RNPJ += int.Parse(_recogError[i].Split()[13]);
                        com[i].RNPK += int.Parse(_recogError[i].Split()[14]);
                        com[i].RNPL += int.Parse(_recogError[i].Split()[15]);


                        com[i].TotalMontado += com[i].NPA + com[i].NPB + com[i].NPC + com[i].NPD + com[i].NPE + com[i].NPF + com[i].NPG + com[i].NPH + com[i].NPI + com[i].NPJ + com[i].NPK + com[i].NPL;
                        com[i].TotalPickupError += com[i].PNPA + com[i].PNPB + com[i].PNPC + com[i].PNPD + com[i].PNPE + com[i].PNPF + com[i].PNPG + com[i].PNPH + com[i].PNPI + com[i].PNPJ + com[i].PNPK + com[i].PNPL;
                        com[i].TotalRecogError += com[i].RNPA + com[i].RNPB + com[i].RNPC + com[i].RNPD + com[i].RNPE + com[i].RNPF + com[i].RNPG + com[i].RNPH + com[i].RNPI + com[i].RNPJ + com[i].RNPK + com[i].RNPL;



                        }
                    }

                for (int i = 1; i <= numeroDeComponentes; i++)
                    {
                    ListaDeComponentes.Add(com[i]);
                    }



                //Executa a atualização do Datagrid no mesmo Thread
                Application.Current.Dispatcher.Invoke(new Action(() => { DataGrid.ItemsSource = ListaDeComponentes; }));



                foreach (var item in ListaDeComponentes)
                    {
                    if (item._porcentagem() >= 1.0)
                        {

                        }

                    }


                }
            catch (Exception ex)
                {
                // MessageBox.Show(ex.Message);
                }
            }

        private void MainWindow1_Loaded_1(object sender, RoutedEventArgs e)
            {


            FileSystemWatcher watcher = new FileSystemWatcher(@"D:\kme\pt200\ProductReport\", "*.mpr");

            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;

            //watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            // watcher.Deleted += Watcher_Deleted;
            // watcher.Renamed += Watcher_Renamed;

            }


        void Watcher_Changed(object sender, FileSystemEventArgs e)
            {

            }
        void Watcher_Renamed(object sender, RenamedEventArgs e)
            {

            }

        void Watcher_Deleted(object sender, FileSystemEventArgs e)
            {

            }

        void Watcher_Created(object sender, FileSystemEventArgs e)
            {
            UltimoMPR = e.FullPath;
            try
                {
                atimer = new System.Timers.Timer();
                atimer.Interval = 2000;
                atimer.Elapsed += Atimer_Elapsed;
                // atimer.AutoReset = true;
                atimer.Enabled = true;

                }
            catch (IOException ea)
                {
                // string lines = File.ReadAllText(SourcePath);


                MessageBox.Show(ea.Message);
                //using (StreamWriter sw = File.AppendText(TargetPath))

                // sw.WriteLine("Ocorreu uma falha");
                }

            }

        private void Atimer_Elapsed(object sender, ElapsedEventArgs e)
            {
            char[] separator = { '\\', '.' };
            string[] UltimoSeparado = UltimoMPR.Split(separator);
            int ultimo = int.Parse(UltimoSeparado[5]);
            int novo = ultimo + 1;

            string TargetPath = @"D:\kme\pt200\ProductReport\CM602-1\" + novo + ".mpr";

            Split_MPR(UltimoMPR, TargetPath);
            }

        private void Button_Click(object sender, RoutedEventArgs e)
            {


            string TargetPath = "D:\\kme\\pt200\\ProductReport\\CM602-1\\xx2.mpr";
            if (File.Exists(TargetPath) == true)
                {

                }
            else
                {
                File.Create(TargetPath);
                }
            }

        }
    }






