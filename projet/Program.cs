using System;
using System.Diagnostics;

namespace projet
{


    public class PositionP4 : Position
    {
        public int[,] tab = new int[6, 7];
        int nbjetonj1 = 21;
        int nbjetonj0 = 21;


        public PositionP4(bool j1aletrait) : base(j1aletrait)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++) { tab[i, j] = 5; }
            }
        } //constructeur


        private Resultat eval;
        public override Resultat Eval
        {
            get { return eval; }
            set
            {
                eval = value; eval = Resultat.indetermine;

                int diag; // représente la somme de quatre cases sur la diagonale.
                int hori; // représente la somme de quatre cases sur la horizontale.
                int verti; // représente la somme de quatre cases sur la verticale.

                // CI-DESSOUS, QUATRE BOUCLES QUI NOUS AIDENT A SAVOIR SI UN JOUEUR A REUSSI A ALIGNE 4 JETONS DE LA MEME COULEUR

                for (int ii = 0; ii < 3; ii++) // PREMIERE BOUCLE POUR 12 CASES
                {
                    for (int jj = 0; jj < 4; jj++)
                    {
                        diag = tab[ii, jj] + tab[ii + 1, jj + 1] + tab[ii + 2, jj + 2] + tab[ii + 3, jj + 3];
                        verti = tab[ii, jj] + tab[ii + 1, jj] + tab[ii + 2, jj] + tab[ii + 3, jj];
                        hori = tab[ii, jj] + tab[ii, jj + 1] + tab[ii, jj + 2] + tab[ii, jj + 3];

                        if (j1aletrait)
                        {
                            if (diag == 4 || hori == 4 || verti == 4) { eval = Resultat.j1gagne; nb = -1; break; }
                        }
                        else
                        {
                            if (diag == 0 || hori == 0 || verti == 0) { eval = Resultat.j0gagne; nb = -1; break; }
                        }
                    }
                }

                for (int ii = 0; ii < 3; ii++) // DEUXIEME BOUCLE POUR 12 CASES
                {
                    for (int jj = 3; jj < 7; jj++)
                    {
                        diag = tab[ii, jj] + tab[ii + 1, jj - 1] + tab[ii + 2, jj - 2] + tab[ii + 3, jj - 3];
                        verti = tab[ii, jj] + tab[ii + 1, jj] + tab[ii + 2, jj] + tab[ii + 3, jj];
                        hori = tab[ii, jj] + tab[ii, jj - 1] + tab[ii, jj - 2] + tab[ii, jj - 3];

                        if (j1aletrait)
                        {
                            if (diag == 4 || hori == 4 || verti == 4) { eval = Resultat.j1gagne; nb = -1; break; }
                        }
                        else
                        {
                            if (diag == 0 || hori == 0 || verti == 0) { eval = Resultat.j0gagne; nb = -1; break; }
                        }
                    }
                }

                for (int ii = 3; ii < 6; ii++) // TROISIEME BOUCLE POUR 12 CASES
                {
                    for (int jj = 0; jj < 4; jj++)
                    {
                        diag = tab[ii, jj] + tab[ii - 1, jj + 1] + tab[ii - 2, jj + 2] + tab[ii - 3, jj + 3];
                        verti = tab[ii, jj] + tab[ii - 1, jj] + tab[ii - 2, jj] + tab[ii - 3, jj];
                        hori = tab[ii, jj] + tab[ii, jj + 1] + tab[ii, jj + 2] + tab[ii, jj + 3];

                        if (j1aletrait)
                        {
                            if (diag == 4 || hori == 4 || verti == 4) { eval = Resultat.j1gagne; nb = -1; break; }
                        }
                        else
                        {
                            if (diag == 0 || hori == 0 || verti == 0) { eval = Resultat.j0gagne; nb = -1; break; }
                        }
                    }
                }

                for (int ii = 3; ii < 6; ii++) // QUATRIEME BOUCLE POUR 12 CASES
                {
                    for (int jj = 3; jj < 7; jj++)
                    {
                        diag = tab[ii, jj] + tab[ii - 1, jj - 1] + tab[ii - 2, jj - 2] + tab[ii - 3, jj - 3];
                        verti = tab[ii, jj] + tab[ii - 1, jj] + tab[ii - 2, jj] + tab[ii - 3, jj];
                        hori = tab[ii, jj] + tab[ii, jj - 1] + tab[ii, jj - 2] + tab[ii, jj - 3];

                        if (j1aletrait)
                        {
                            if (diag == 4 || hori == 4 || verti == 4) { eval = Resultat.j1gagne; nb = -1; break; }
                        }
                        else
                        {
                            if (diag == 0 || hori == 0 || verti == 0) { eval = Resultat.j0gagne; nb = -1; break; }
                        }
                    }
                }

                if ((nbjetonj0 == 0 && nbjetonj1 == 0) && eval == Resultat.indetermine) // AU CAS OU LA PARTIE EST NULLE
                {
                    eval = Resultat.partieNulle; nb = -1;
                }


            }
        }

        private int nb;
        public override int NbCoups
        {
            get { return nb; }
            set
            {
                if (nb != -1) // ON N'EST PAS DANS UNE POSITION FINALE. LE NOMBRE -1 ME PERMET DE FAIRE LE LIEN AVEC LA PROPRIETE "Eval"
                {
                    nb = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        if (tab[0, j] == 5) { nb++; } // si nb =0 alors il n'y a plus de jetons 
                    }
                }
                else { nb = 0; } // ON EST DANS UNE POSITION FINALE: il y a un gagant ou la partie est nulle

                if (value == 15) { nb = 7; } // IL VA PERMETTRE D'EFFECTUER UN COUP CORRECTEMENT DANS LA BOUCLE WHILE DE "JeuHasard"
            }
        }

        public override void EffectuerCoup(int i) // elle doit mettre à jour les propriétés Eval et NbCoups
        {
            /// i REPRESENTE LA COLONNE CHOISIE 

            int[] tabb = new int[7];
            for (int k = 0; k < 7; k++)
            {
                tabb[k] = 9; // nombre choisi car il était différent de 0, 1, ..., 5
            }

            while (tab[0, i] == 0 || tab[0, i] == 1) // SI LA MACHINE CHOISIT UNE COLONNE REMPLIE ALORS ON GENERE UN NOUVEAU NOMBRE ALEATOIRE. CETTE BOUCLE ME PERMET DE SORTIR DE LA BOUCLE WHILE DE LA METHODE "JeuHasard"
            {
                tabb[i] = i;

                Random genn = new Random();
                i = genn.Next(0, 7); // L'INDICE DU COUP EST REMPLACE SI LA COLONNE CHOISIE EST REMPLIE PAR DES 0 ET 1.

                if (tabb[0] == 0 && tabb[1] == 1 && tabb[2] == 2 && tabb[3] == 3 && tabb[4] == 4 && tabb[5] == 5 && tabb[6] == 6) // ON RENTRE JAMAIS DANS CETTE BOUCLE. IL M'A AIDE A VERIFIER SI J'AVAIS BIEN CONSTRUIT LA METHODE CLONE.
                {
                    Console.WriteLine("-------------------------IL Y A UN PROBLEME -------------------");
                    break;
                }

            }

            for (int ii = 5; ii >= 0; ii--) // ON MET A JOUR LE NOMBRE DE JETONS DISPONIBLES DE CHAQUE JOUEUR
            {
                if (tab[ii, i] == 5)
                {
                    if (j1aletrait) { tab[ii, i] = 1; nbjetonj1--; break; }
                    else { tab[ii, i] = 0; nbjetonj0--; break; }
                }
            }

            Eval = Resultat.indetermine;
            NbCoups = 7;

            j1aletrait = !(j1aletrait);

        }


        public override Position Clone()
        {
            PositionP4 p4 = new PositionP4(j1aletrait);

            p4.nbjetonj0 = nbjetonj0;
            p4.nbjetonj1 = nbjetonj1;
            p4.tab = (int[,])tab.Clone();

            p4.Eval = Resultat.indetermine;
            p4.NbCoups = NbCoups;

            return p4;
        }

        public override void Affiche()
        {

            if (j1aletrait)
            {
                if (Eval == Resultat.indetermine)
                {
                    Console.WriteLine("\nLe joueur J1 doit jouer.");
                }
                else { Console.WriteLine("\nFIN DU JEU:"); }
            }
            else
            {
                if (Eval == Resultat.indetermine)
                {
                    Console.WriteLine("\nLe joueur J0 doit jouer.");
                }
                else { Console.WriteLine("\nFIN DU JEU:"); }
            }

            Console.WriteLine("Nombre de jetons restants du joueur j0 = {0} et nombre de jetons restants du joueur j1 = {1}.", nbjetonj0, nbjetonj1);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tab[i, j] != 5) { Console.Write("tab[{0},{1}]={2} ", i, j, tab[i, j]); }
                    else { Console.Write("tab[{0},{1}]=  ", i, j); }

                    if (j == 6) { Console.WriteLine(""); }
                }
            }

        }

    }



    public class JoueurHumainPuissance : Joueur
    {
        public override int Jouer(Position p)
        {
            PositionP4 petit = (PositionP4)p.Clone();

            p.NbCoups = 6;
            Console.WriteLine("JOUER: choisissez l'indice d'une colonne entre 0 et 6. Il y a {0} colonnes disponibles.", p.NbCoups);

            int indiceCoup;
            while (!Int32.TryParse(Console.ReadLine(), out indiceCoup) || indiceCoup < 0 || indiceCoup > 6 || (petit.tab)[0, indiceCoup] != 5)
            {
                Console.WriteLine("VEUILLEZ REESSAYER S'IL VOUS PLAIT.");
            }

            return indiceCoup;
        }

    }



    //////// // ///// ////////   //////// // ///// ////////  //////// // ///// ////////

    public class PositionA : Position
    {
        public int NbAllumette = 8; // changer le nombre comme vous voulez !

        public PositionA(bool j1aletrait) : base(j1aletrait) { } //constructeur

        private Resultat eval;
        public override Resultat Eval
        { // peu importe la valeur en écriture
            get => eval;
            set
            {
                if (NbAllumette > 0) { eval = value; eval = Resultat.indetermine; }
                else
                {
                    if (j1aletrait) { eval = value; eval = Resultat.j1gagne; }
                    else { eval = value; eval = Resultat.j0gagne; }
                }
            }
        }

        private int nb;
        public override int NbCoups
        { // peu importe la valeur en écriture
            get { return nb; }
            set
            {
                if (NbAllumette >= 3) { nb = value; nb = 3; }
                else
                {
                    if (NbAllumette == 2) { nb = value; nb = 2; }
                    if (NbAllumette == 1) { nb = value; nb = 1; }
                    if (NbAllumette == 0) { nb = value; nb = 0; }
                }
            }
        }

        public override void EffectuerCoup(int i) // elle doit mettre à jour les propriétés Eval et NbCoups
        {
            NbAllumette = NbAllumette - (i + 1); //  modifie Eval et Nbcoups  
            j1aletrait = (!j1aletrait); // modifie j1aletrait

            this.NbCoups = 7; // peu importe la valeur; c'est pour connaitre le nb de allumettes restantes
            this.Eval = Resultat.indetermine; // peu importe la valeur; c'est pour savoir s'il y a un gagnant
        }

        public override Position Clone()
        {
            PositionA pa = new PositionA(j1aletrait);

            pa.NbAllumette = NbAllumette;
            pa.NbCoups = 3;
            pa.Eval = Resultat.indetermine;

            return pa;
        }

        public override void Affiche()
        {
            if (j1aletrait)
            {
                if (Eval == Resultat.indetermine)
                {
                    Console.WriteLine("\nLe joueur J1 doit jouer.");
                }
                else { Console.WriteLine("\nFIN DU JEU:"); }
            }
            else
            {
                if (Eval == Resultat.indetermine)
                {
                    Console.WriteLine("\nLe joueur J0 doit jouer.");
                }
                else { Console.WriteLine("\nFIN DU JEU:"); }
            }

            if (NbAllumette > 1) { Console.WriteLine("Il y a {0} allumettes.", NbAllumette); }
            else { Console.WriteLine("Il y a {0} allumette.", NbAllumette); }
        }

    }


    public class JoueurHumainA : Joueur
    {

        public override int Jouer(Position p)
        {
            p.NbCoups = 3;

            if (p.NbCoups == 3)
            {
                Console.WriteLine("JOUER: choisissez l'indice du coup entre 0 et {0}:", p.NbCoups - 1);
                Console.WriteLine(" - indiceCoup = 0 implique que le joueur prend 1 allumette.");
                Console.WriteLine(" - indiceCoup = 1 implique que le joueur prend 2 allumettes.");
                Console.WriteLine(" - indiceCoup = 2 implique que le joueur prend 3 allumettes.");
            }
            if (p.NbCoups == 2)
            {
                Console.WriteLine("JOUER: choisissez l'indice du coup entre 0 et {0}:", p.NbCoups - 1);
                Console.WriteLine(" - indiceCoup = 0 implique que le joueur prend 1 allumette.");
                Console.WriteLine(" - indiceCoup = 1 implique que le joueur prend 2 allumettes.");
            }
            if (p.NbCoups == 1)
            {
                Console.WriteLine("JOUER: vous ne pouvez choisir que l'indice du coup égal à 0.");
            }

            int indiceCoup;
            while (!Int32.TryParse(Console.ReadLine(), out indiceCoup) || indiceCoup < 0 || indiceCoup >= p.NbCoups)
            {
                Console.WriteLine("VEUILLEZ REESSAYER S'IL VOUS PLAIT.");
            }
            return indiceCoup;
        }





    }




    /// /////////////////////////////////  //////// // ///// ////////  //////// // ///// ////////

    public enum Resultat { j1gagne, j0gagne, partieNulle, indetermine }

    public abstract class Position
    {
        public bool j1aletrait;
        public Position(bool j1aletrait) { this.j1aletrait = j1aletrait; }
        public virtual Resultat Eval { get; set; } // J'AI AJOUTE LE MOT CLE "VIRTUAL", ET J'AI EFFACE LE MOT CLE "PROTECTED" AVANT LE MOT "SET" 
        public virtual int NbCoups { get; set; } // J'AI AJOUTE LE MOT CLE "VIRTUAL", ET J'AI EFFACE LE MOT CLE "PROTECTED" AVANT LE MOT "SET"
        public abstract void EffectuerCoup(int i);
        public abstract Position Clone();
        public abstract void Affiche();
    }


    public abstract class Joueur
    {
        public abstract int Jouer(Position p);
        public virtual void NouvellePartie() { }
    }



    public class Partie
    {
        Position pCourante;
        Joueur j1, j0;
        public Resultat r;

        public Partie(Joueur j1, Joueur j0, Position pInitiale)
        {
            this.j1 = j1;
            this.j0 = j0;
            pCourante = pInitiale.Clone();
        }

        public void NouveauMatch(Position pInitiale)
        {
            pCourante = pInitiale.Clone();
        }

        public void Commencer(bool affichage = true)
        {
            j1.NouvellePartie();
            j0.NouvellePartie();
            do
            {
                if (affichage) pCourante.Affiche();
                if (pCourante.j1aletrait)
                {
                    pCourante.EffectuerCoup(j1.Jouer(pCourante.Clone()));
                }
                else
                {
                    pCourante.EffectuerCoup(j0.Jouer(pCourante.Clone()));
                }
            } while (pCourante.NbCoups > 0);
            r = pCourante.Eval;
            if (affichage)
            {
                pCourante.Affiche();
                switch (r)
                {
                    case Resultat.j1gagne: Console.WriteLine("j1 {0} a gagné.", j1); break;
                    case Resultat.j0gagne: Console.WriteLine("j0 {0} a gagné.", j0); break;
                    case Resultat.partieNulle: Console.WriteLine("Partie nulle."); break;
                }
            }
        }
    }

    public class Noeud
    {
        static Random gen = new Random();

        public Position p;
        public Noeud pere;
        public Noeud[] fils;
        public int cross, win;
        public int indiceMeilleurFils;

        public Noeud(Noeud pere, Position p)
        {
            this.pere = pere;
            this.p = p; //p.NbCoups = 15; //
            fils = new Noeud[this.p.NbCoups];
        }

        public void CalculMeilleurFils(Func<int, int, float> phi)
        {
            float s;
            float sM = 0;
            if (p.j1aletrait)
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(fils[i].win, fils[i].cross); }

                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
            else
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(fils[i].cross - fils[i].win, fils[i].cross); }

                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
        }


        public Noeud MeilleurFils()
        {
            if (fils[indiceMeilleurFils] != null)
            {
                return fils[indiceMeilleurFils];
            }
            else
            {
                Position q = p.Clone();

                ////// J'AI AJOUTE CETTE PARTIE POUR PUISSANCE 4. LA MACHINE EFFECTURA UN COUP CORRECTEMENT, C-A-D, ELLE PRENDRA UNE COLONNE QUI N'EST PAS REMPLIE ENTIEREMENT

                if (q is PositionP4) // SI ON SE TROUVE DANS LE JEU PUISSANCE 4
                {

                    q.NbCoups = 40;
                    int[] tabI2 = new int[q.NbCoups]; // il va représenter: TabI[ indice noeud ] = indice de la colonne

                    int[] caise = new int[2]; // caise[0] représente le nombre de colonnes non remplies entièrement jusqu'à une certaine colonne
                                              //caise[1] représente le nombre de colonnes remplies

                    // caiseN = caise[1] ;  caiseB = caise[0];

                    for (int aa = 0; aa < q.NbCoups; aa++)
                    {
                        caise[0] = 0; caise[1] = 0;

                        for (int bb = 0; bb < 7; bb++)
                        {
                            if (((PositionP4)q).tab[0, bb] == 5)
                            {
                                caise[0] = caise[0] + 1;
                            }
                            else
                            {
                                caise[1] = caise[1] + 1;
                            }

                            if (aa < caise[0]) { break; }

                        }

                        tabI2[aa] = aa + caise[1];

                    }

                    q.EffectuerCoup(tabI2[indiceMeilleurFils]);

                }
                else q.EffectuerCoup(indiceMeilleurFils); // SI ON SE TROUVE DANS LE JEU DES ALLUMETTES

                //////////////////////

                //q.EffectuerCoup(indiceMeilleurFils); avant

                fils[indiceMeilleurFils] = new Noeud(this, q);
                return fils[indiceMeilleurFils];
            }
        }

        public override string ToString()
        {
            string s = "";
            s = s + "indice MF = " + indiceMeilleurFils;
            s += String.Format(" note= {0}\n", fils[indiceMeilleurFils] == null ? "?" : ((1F * fils[indiceMeilleurFils].win) / fils[indiceMeilleurFils].cross).ToString());
            int sc = 0;
            for (int k = 0; k < fils.Length; k++)
            {
                if (fils[k] != null)
                {
                    sc += fils[k].cross;
                    s += (fils[k].win + "/" + fils[k].cross + " ");
                }
                else s += (0 + "/" + 0 + " ");
            }
            s += "\n nbC=" + (sc / 2);
            return s;
        }

    }


    public class JMCTS : Joueur
    {
        public static Random gen = new Random();
        static Stopwatch sw = new Stopwatch();

        float a, b;
        int temps;

        Noeud racine;

        public JMCTS(float a, float b, int temps)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }

        int JeuHasard(Position p)
        {
            Position q = p.Clone();

            int re = 1;
            while (q.NbCoups > 0)
            {
                if (q is PositionP4) // J'AI AJOUTE CETTE PARTIE POUR PUISSANCE4
                {
                    q.NbCoups = 15; // il va me permettre de générer un nombre aléatoire entre 0 et 6  
                }

                q.EffectuerCoup(gen.Next(0, q.NbCoups));

                if (q is PositionP4) // J'AI AJOUTER CETTE PARTIE
                {
                    q.Eval = Resultat.indetermine; // Important pour NbCoups
                    q.NbCoups = 25; // je détermine la vraie valeur de NbCoups
                }

            }

            if (q.Eval == Resultat.j1gagne) { re = 2; }
            if (q.Eval == Resultat.j0gagne) { re = 0; }
            return re;
        }


        public override int Jouer(Position p)
        {
            sw.Restart();
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);

            racine = new Noeud(null, p);
            int iter = 0;
            while (sw.ElapsedMilliseconds < temps)
            {
                Noeud no = racine;

                do // Sélection
                {
                    no.CalculMeilleurFils(phi);
                    no = no.MeilleurFils();

                } while (no.cross > 0 && no.fils.Length > 0);

                //Console.WriteLine("FIN SELECTION");
                int re = JeuHasard(no.p); // Simulation 
                //Console.WriteLine("FIN SIMULATION");

                while (no != null) // Rétropropagation
                {
                    no.cross += 2;
                    no.win += re;
                    no = no.pere;
                }
                iter++;
            }
            racine.CalculMeilleurFils(phi);

            Console.WriteLine("{0} itérations", iter);

            Console.WriteLine(racine);



            /////  J'AI AJOUTE CETTE PARTIE POUR PUISSANCE 4. LA MACHINE EFFECTURA UN COUP CORRECTEMENT, C-A-D, ELLE PRENDRA UNE COLONNE QUI N'EST PAS REMPLIE ENTIEREMENT

            if (racine.p is PositionP4) // SI ON SE TROUVE DANS LE JEU PUISSANCE 4
            {
                int[] tabI = new int[racine.p.NbCoups];

                int[] caise = new int[2]; // caise[0] représente le nombre de colonnes non remplies entièrement jusqu'à une certaine colonne
                                          //caise[1] représente le nombre de colonnes remplies

                // caiseN = caise[1] ;  caiseB = caise[0];

                for (int aa = 0; aa < racine.p.NbCoups; aa++)
                {
                    caise[0] = 0; caise[1] = 0;

                    for (int bb = 0; bb < 7; bb++)
                    {
                        if (((PositionP4)racine.p).tab[0, bb] == 5)
                        {
                            caise[0] = caise[0] + 1;
                        }
                        else
                        {
                            caise[1] = caise[1] + 1;
                        }

                        if (aa < caise[0]) { break; }

                    }

                    tabI[aa] = aa + caise[1];

                }

                Console.WriteLine("   tabI[indice du MF]= indice de la colonne");
                for (int tt = 0; tt < racine.p.NbCoups; tt++)
                {
                    Console.Write("   tabI[{0}]={1} ", tt, tabI[tt]);
                }

                Console.WriteLine("\nL'indice de la colonne choisie est : {0} ", tabI[racine.indiceMeilleurFils]);

                return tabI[racine.indiceMeilleurFils];

            }
            else return racine.indiceMeilleurFils; // SI ON SE TROUVE DANS LE JEU DES ALLUMETTES


        }
    }


    class MainClass
    {
        public static void Main()
        {
            ////////////////////       PARTIE ALLUMETTES    ////////////////////

            ////// 1. JEU ENTRE DEUX JOUEUR HUMAINS

            /*
            Console.Clear();

            JoueurHumainA j1 = new JoueurHumainA();
            JoueurHumainA j0 = new JoueurHumainA(); 
          
            PositionA ini = new PositionA(true);
            
            Partie partie = new Partie(j1 ,j0 , ini);
            partie.NouveauMatch(ini);
            partie.Commencer(true);
            */

            //////// 2. JEU ENTRE UN JOUEUR HUMAIN ET UN JOUEUR JMCTS

            /*
            Console.Clear();
            JoueurHumainA j1 = new JoueurHumainA();           
            JMCTS j0 = new JMCTS(2, 2, 8);
            Console.WriteLine(j0.ToString());

            PositionA ini = new PositionA(true);
            Partie partie = new Partie(j1, j0, ini);

            partie.NouveauMatch(ini);
            partie.Commencer(true);
            */


            ///////// 3. JEU ENTRE DEUX JOUEUR JMCTS

            /*
            Console.Clear();
            JMCTS j1 = new JMCTS(2, 2, 8);
            JMCTS j0 = new JMCTS(2, 2, 8);
            
            PositionA ini = new PositionA(true);
            Partie partie = new Partie(j1, j0, ini);

            partie.NouveauMatch(ini);
            partie.Commencer(true);

            */
            

            //////////////////////       PARTIE PUISSANCE 4    ////////////////////


            ////// 1. JEU ENTRE DEUX JOUEUR HUMAINS

            /*
            Console.Clear();
            JoueurHumainPuissance j1 = new JoueurHumainPuissance();
            JoueurHumainPuissance j0 = new JoueurHumainPuissance();

            PositionP4 ini = new PositionP4(true);
            //Console.WriteLine("nbcoups= {0} ", ini.NbCoups ); ini.NbCoups=3;

            Partie partie = new Partie(j1, j0, ini);
            partie.NouveauMatch(ini);
            partie.Commencer(true);
            
            */

            //////// 2. JEU ENTRE UN JOUEUR HUMAIN ET UN JOUEUR JMCTS

            /*
            Console.Clear();
            JoueurHumainPuissance j1 = new JoueurHumainPuissance();
            JMCTS j0 = new JMCTS(2, 2, 50);
            Console.WriteLine(j0.ToString());

            PositionP4 ini = new PositionP4(true);
            Partie partie = new Partie(j1, j0, ini);

            partie.NouveauMatch(ini);
            partie.Commencer(true);
            */

            ///////// 3. JEU ENTRE DEUX JOUEUR JMCTS

            
            Console.Clear();
            JMCTS j1 = new JMCTS(2, 2, 50);
            JMCTS j0 = new JMCTS(2, 2, 50);
            Console.WriteLine(j0.ToString());

            PositionP4 ini = new PositionP4(true);
            Partie partie = new Partie(j1, j0, ini);

            partie.NouveauMatch(ini);
            partie.Commencer(true);
            




            /////// 4.  A LA RECHERCHE D'UN "a" OPTIMALE. ATTENTION LA MACHINE PREND TROP DE TEMPS POUR TERMINER L'EXECUTION


            /*
            
            Console.Clear();
            int a ;
            int nombre = 50;
            int[] tabA = new int[nombre];  // Il va symboliser le nombre de victoires par rapport à la valeur de a. Chaque indice du tableau représente une certaine valeur de a: l'indice 0 implique que a=0, l'indice 1 implique que a= 1, et ainsi de suite         

            PositionP4 ini = new PositionP4(true);

            for (a=0; a < nombre; a++)
            {
                int r = 0;
                JMCTS j1 = new JMCTS(a, a, 100);

                for (int k = 0; k < nombre; k++)
                {
                    JMCTS j0 = new JMCTS(k, k, 100);

                    Partie partie = new Partie(j1, j0, ini);

                    partie.NouveauMatch(ini);
                    partie.Commencer(true);

                    if (partie.r == Resultat.j1gagne) { r = r + 1; }

                }
                tabA[a] = r ; 
            }


            for (int jj=0; jj<nombre; jj++ )
            {
                Console.Write("tabA[{0}] = {1} ",jj, tabA[jj]); // nombre de victoires par rapport à la valeur de a
            }
            */



        }
    }

}





