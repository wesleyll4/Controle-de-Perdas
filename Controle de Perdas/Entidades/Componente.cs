using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Controle_de_Perdas.Entidades
    {
    class Componente
        {
        public string Maquina { get; set; }
        public string Addres { get; set; }
        public string SubAdr { get; set; }
        public string Endereço { get; set; }
        public string PN { get; set; }
        public int NPA { get; set; }
        public int NPB { get; set; }
        public int NPC { get; set; }
        public int NPD { get; set; }
        public int NPE { get; set; }
        public int NPF { get; set; }
        public int NPG { get; set; }
        public int NPH { get; set; }
        public int NPI { get; set; }
        public int NPJ { get; set; }
        public int NPK { get; set; }
        public int NPL { get; set; }

        public int PNPA { get; set; }
        public int PNPB { get; set; }
        public int PNPC { get; set; }
        public int PNPD { get; set; }
        public int PNPE { get; set; }
        public int PNPF { get; set; }
        public int PNPG { get; set; }
        public int PNPH { get; set; }
        public int PNPI { get; set; }
        public int PNPJ { get; set; }
        public int PNPK { get; set; }
        public int PNPL { get; set; }

        public int RNPA { get; set; }
        public int RNPB { get; set; }
        public int RNPC { get; set; }
        public int RNPD { get; set; }
        public int RNPE { get; set; }
        public int RNPF { get; set; }
        public int RNPG { get; set; }
        public int RNPH { get; set; }
        public int RNPI { get; set; }
        public int RNPJ { get; set; }
        public int RNPK { get; set; }
        public int RNPL { get; set; }

        public double TotalMontado { get; set; }
        public int TotalPickupError { get; set; }
        public int TotalRecogError { get; set; }
        public double TotalPerdido { get; set; }
        public string Porcentagem { get; set; }
        public string State { get; set; }

        public Componente()
            {

            }
        public double _TotalPerdido()
            {
            return TotalPerdido += TotalPickupError + TotalRecogError;
            }

        public double _porcentagem()
            {
            return TotalPerdido * 100.0 / TotalMontado;
            }


        public Componente(string pN, int nPA, int nPB, int nPC, int nPD, int nPE, int nPF, int nPG, int nPH, int nPI, int nPJ, int nPK, int nPL, string addres, string subAdr)
            {
            PN = pN;
            NPA = nPA;
            NPB = nPB;
            NPC = nPC;
            NPD = nPD;
            NPE = nPE;
            NPF = nPF;
            NPG = nPG;
            NPH = nPH;
            NPI = nPI;
            NPJ = nPJ;
            NPK = nPK;
            NPL = nPL;
            Addres = addres;
            SubAdr = subAdr;

            }

        public Componente(string addres, string subAdr, string pN, int nPA, int nPB, int nPC, int nPD, int nPE, int nPF, int nPG, int nPH, int nPI, int nPJ, int nPK, int nPL, int pNPA, int pNPB, int pNPC, int pNPD, int pNPE, int pNPF, int pNPG, int pNPH, int pNPI, int pNPJ, int pNPK, int pNPL, int rNPA, int rNPB, int rNPC, int rNPD, int rNPE, int rNPF, int rNPG, int rNPH, int rNPI, int rNPJ, int rNPK, int rNPL)
            {
            Addres = addres;
            SubAdr = subAdr;
            PN = pN;
            NPA = nPA;
            NPB = nPB;
            NPC = nPC;
            NPD = nPD;
            NPE = nPE;
            NPF = nPF;
            NPG = nPG;
            NPH = nPH;
            NPI = nPI;
            NPJ = nPJ;
            NPK = nPK;
            NPL = nPL;
            PNPA = pNPA;
            PNPB = pNPB;
            PNPC = pNPC;
            PNPD = pNPD;
            PNPE = pNPE;
            PNPF = pNPF;
            PNPG = pNPG;
            PNPH = pNPH;
            PNPI = pNPI;
            PNPJ = pNPJ;
            PNPK = pNPK;
            PNPL = pNPL;
            RNPA = rNPA;
            RNPB = rNPB;
            RNPC = rNPC;
            RNPD = rNPD;
            RNPE = rNPE;
            RNPF = rNPF;
            RNPG = rNPG;
            RNPH = rNPH;
            RNPI = rNPI;
            RNPJ = rNPJ;
            RNPK = rNPK;
            RNPL = rNPL;
            }

        public override string ToString()
            {
            return "Part Number: "+ PN
                + "|| Endereço: " + Addres+" "+ SubAdr+
                "|| Montados: " + TotalMontado + 
                "|| Erros de Pickup: " + TotalPickupError 
                + "|| Erros de Recog: " + TotalRecogError + 
                "|| Perdas: " + (TotalRecogError + TotalPickupError) ;
            }

            }

        }
