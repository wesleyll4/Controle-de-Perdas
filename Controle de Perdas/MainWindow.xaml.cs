using Controle_de_Perdas.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controle_de_Perdas
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        private string UltimoMPR = null;
        // Instancia uma lista de componentes
        private List<Componente> ListaDeComponentes = new List<Componente>();


        public MainWindow()
            {
            InitializeComponent();
            var Diretorio = new DirectoryInfo(@"D:\kme\pt200\ProductReport\");

            try
                {
                var LastFile = (from f in Diretorio.GetFiles("*.mpr", SearchOption.AllDirectories)
                                orderby f.LastWriteTime descending
                                select f).First().FullName.ToString();

                Split_MPR(LastFile);
                }
            catch
                {

                }

            }

        private void Split_MPR(string SourcePath)
            {
            try
                {
                // Lê todo o documento fonte e salva as linhas em uma string
                string lines = File.ReadAllText(SourcePath);

                // Define os caracters usados para separar as strings do arquivo mpr
                char[] delimiterChars = { '[', ']' };

                //Divide o as strings do arquivo usando os separadores
                string[] words = lines.Split(delimiterChars);

                // Seprar Todo o string lido em partes de PN
                string contagem = words[11] + words[12];
                string[] _contagem = contagem.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                string PickupError = words[13] + words[14];
                string[] _pickUpError = PickupError.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                string RecogError = words[15] + words[16];
                string[] _recogError = RecogError.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                string Count = null;

                for (int i = 1; i < _contagem.Length; i++)
                    {
                    Count += _contagem[i];
                    }

                string[] Componentes = Count.Split(' ');

                int numeroDeComponentes = Componentes.Length / 60;

                string[] maqx = SourcePath.Split( '\\' );
                string maq = maqx[4].Substring(6);


                Componente[] com = new Componente[_contagem.Length];

                // Atribui os dados do string as propriedades do objeto Componente
                for (int i = 2; i < _contagem.Length - 2; i++)
                    {
                    com[i] = new Componente();


                    com[i].Maquina = maq;
                    com[i].Tbl = _contagem[i].Substring(0, 1);
                    com[i].Addres = _contagem[i].Substring(3, 2);
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

                    for (int j = 1; j < _pickUpError.Length - 2; j++)
                        {
                        if (_pickUpError[j].Split()[3] == com[i].PN)
                            {
                            com[i].PNPA += int.Parse(_pickUpError[j].Split()[4]);
                            com[i].PNPB += int.Parse(_pickUpError[j].Split()[5]);
                            com[i].PNPC += int.Parse(_pickUpError[j].Split()[6]);
                            com[i].PNPD += int.Parse(_pickUpError[j].Split()[7]);
                            com[i].PNPE += int.Parse(_pickUpError[j].Split()[8]);
                            com[i].PNPF += int.Parse(_pickUpError[j].Split()[9]);
                            com[i].PNPG += int.Parse(_pickUpError[j].Split()[10]);
                            com[i].PNPH += int.Parse(_pickUpError[j].Split()[11]);
                            com[i].PNPI += int.Parse(_pickUpError[j].Split()[12]);
                            com[i].PNPJ += int.Parse(_pickUpError[j].Split()[13]);
                            com[i].PNPK += int.Parse(_pickUpError[j].Split()[14]);
                            com[i].PNPL += int.Parse(_pickUpError[j].Split()[15]);
                            }
                        else
                            {
                            com[i].PNPA += 0;
                            com[i].PNPB += 0;
                            com[i].PNPC += 0;
                            com[i].PNPD += 0;
                            com[i].PNPE += 0;
                            com[i].PNPF += 0;
                            com[i].PNPG += 0;
                            com[i].PNPH += 0;
                            com[i].PNPI += 0;
                            com[i].PNPJ += 0;
                            com[i].PNPK += 0;
                            com[i].PNPL += 0;
                            }
                        }

                    for (int j = 1; j < _recogError.Length - 2; j++)
                        {
                        if (_recogError[j].Split()[3] == com[i].PN)
                            {
                            com[i].RNPA += int.Parse(_recogError[j].Split()[4]);
                            com[i].RNPB += int.Parse(_recogError[j].Split()[5]);
                            com[i].RNPC += int.Parse(_recogError[j].Split()[6]);
                            com[i].RNPD += int.Parse(_recogError[j].Split()[7]);
                            com[i].RNPE += int.Parse(_recogError[j].Split()[8]);
                            com[i].RNPF += int.Parse(_recogError[j].Split()[9]);
                            com[i].RNPG += int.Parse(_recogError[j].Split()[10]);
                            com[i].RNPH += int.Parse(_recogError[j].Split()[11]);
                            com[i].RNPI += int.Parse(_recogError[j].Split()[12]);
                            com[i].RNPJ += int.Parse(_recogError[j].Split()[13]);
                            com[i].RNPK += int.Parse(_recogError[j].Split()[14]);
                            com[i].RNPL += int.Parse(_recogError[j].Split()[15]);
                            }
                        else
                            {
                            com[i].RNPA += 0;
                            com[i].RNPB += 0;
                            com[i].RNPC += 0;
                            com[i].RNPD += 0;
                            com[i].RNPE += 0;
                            com[i].RNPF += 0;
                            com[i].RNPG += 0;
                            com[i].RNPH += 0;
                            com[i].RNPI += 0;
                            com[i].RNPJ += 0;
                            com[i].RNPK += 0;
                            com[i].RNPL += 0;
                            }
                        }


                    com[i].TotalMontado += com[i].NPA + com[i].NPB + com[i].NPC + com[i].NPD + com[i].NPE + com[i].NPF + com[i].NPG + com[i].NPH + com[i].NPI + com[i].NPJ + com[i].NPK + com[i].NPL;
                    com[i].TotalPickupError += com[i].PNPA + com[i].PNPB + com[i].PNPC + com[i].PNPD + com[i].PNPE + com[i].PNPF + com[i].PNPG + com[i].PNPH + com[i].PNPI + com[i].PNPJ + com[i].PNPK + com[i].PNPL;
                    com[i].TotalRecogError += com[i].RNPA + com[i].RNPB + com[i].RNPC + com[i].RNPD + com[i].RNPE + com[i].RNPF + com[i].RNPG + com[i].RNPH + com[i].RNPI + com[i].RNPJ + com[i].RNPK + com[i].RNPL;

                    com[i].TotalPerdido += com[i]._TotalPerdido();
                    com[i].Porcentagem = Math.Round(com[i]._porcentagem(), 2);

                    // Condicional para mudar a cor dos componentes com Perdas acima de 1%
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

                // Adiciona os itens de com[] a uma lista
                for (int i = 0; i <= numeroDeComponentes; i++)
                    {
                    if (com[i] != null)
                        {
                        var c = ListaDeComponentes.FirstOrDefault(d => d.PN == com[i].PN && d.Addres == com[i].Addres && d.Maquina == com[i].Maquina);

                        if (c != null)
                            {
                            if (com[i].Porcentagem > c.Porcentagem && c.Porcentagem != 0.0)
                                {
                                com[i].Status = "Porcentagem Subindo";
                                }
                            else if  (com[i].Porcentagem < c.Porcentagem && c.Porcentagem != 0.0)
                                {
                                com[i].Status = "Porcentagem baixando";
                                }
                            else if (com[i].Porcentagem == c.Porcentagem && c.Porcentagem != 0.0)
                                {
                                com[i].Status = "Porcentagem baixando";
                                }

                            ListaDeComponentes.Remove(c);
                            ListaDeComponentes.Add(com[i]);
                            }
                        else
                            {
                            ListaDeComponentes.Add(com[i]);
                            }
                        }
                    }


                var list = ListaDeComponentes.OrderByDescending(y => y.Porcentagem);

                //Executa a atualização do Datagrid no mesmo Thread
                Application.Current.Dispatcher.Invoke(new Action(() => { DataGrid.ItemsSource = list; }));
                }
            //Tratar Exceptions
            catch (Exception e1)
                {
                MessageBox.Show(e1.Message, "split ");
                }
            }

        private void MainWindow1_Loaded_1(object sender, RoutedEventArgs e)
            {
            // Instancia o objeto a ser observado
            FileSystemWatcher watcher = new FileSystemWatcher(@"D:\kme\pt200\ProductReport\", "*.mpr");

            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;

            watcher.Created += Watcher_Created;
            }

        private async void Watcher_Created(object sender, FileSystemEventArgs e)
            {
            await Task.Delay(2000);
            UltimoMPR = e.FullPath;

            try
                {

                Split_MPR(UltimoMPR);

                }
            catch (IOException ea)
                {
                MessageBox.Show(ea.Message, "Watcher");
                }
            }
        }
    }