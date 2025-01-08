using System;
using System.Runtime.Serialization.Formatters;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Morpion
{
    class Program
    {
        public static int[,] grille = new int[3, 3];

        // Affichage de la grille du Morpion
        public static void AfficherMorpion()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grille[i, j] == 1) // Permet d'ajouter 'X' à une case vide
                        Console.Write(" X ");
                    else if (grille[i, j] == 2)
                        Console.Write(" O "); // Permet d'ajouter 'O' à une case vide
                    else
                        Console.Write(" . "); // Permet de mettre un '.' en attendant qu'un joueur joue son tour

                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("---+---+---");
            }
        }

        // Fonction permettant de changer
        // dans le tableau quel est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int l, int c, int joueur)
        {
            Console.Clear();
            if (l < 0 || l >= 3 || c < 0 || c >= 3)
            {
                Console.WriteLine("Vous etes en dehors des limites de la grille !"); // Message pour afficher l'erreur de placement
                return false;
            }

            if (grille[c, l] != 10)
            {
                Console.WriteLine("La case séléctionné est déjà occupée !"); // Message pour afficher que le case est déjà remplie
                return false;
            }

            grille[c, l] = joueur;
            return true;
        }
        // Fonction permettant de vérifier
        // si un joueur à gagner
        public static bool Gagner(int joueur)
        {
            for (int i = 0; i < 3; i++)
            {
                // Vérificaton de l'alignement de 3 caractères
                if ((grille[i, 0] == joueur && grille[i, 1] == joueur && grille[i, 2] == joueur) || (grille[0, i] == joueur && grille[1, i] == joueur && grille[2, i] == joueur))
                {
                    return true;
                }
            }
            if ((grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur) || (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur))
            {
                return true;
            }
            return false;
        }
        public static bool VerifEgal()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grille[i, j] == 10) // Si une case est vide, il n'y a pas d'égalité donc on laisse false à gagner
                        return false;
                }
            }
            return true; 
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations ---

            int essais = 1;          // compteur d'essais que l'on initialise à 1
            int joueur = 1;         // 1 pour la premier joueur, 2 pour le second
            int l, c = 0;          // numéro de ligne et de colonne
            int i, j = 0;         // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné

            //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10

            for (j = 0; j < grille.GetLength(0); j++)
                for (i = 0; i < grille.GetLength(1); i++)
                    grille[j, i] = 10;
            while (!gagner && essais != 10)
            {
                AfficherMorpion();
                Console.WriteLine($"Joueur n°{joueur}, c'est à vous de jouer !"); // Affiche ce message tant que personne n'a gagné
                try

                {
                    Console.Write("Ligne (1-3) : ");
                    l = int.Parse(Console.ReadLine()) - 1; // Permet d'optimiser l'emplacement du curseur lors d'affichage de message sur la console

                    Console.Write("Colonne (1-3) : ");
                    c = int.Parse(Console.ReadLine()) - 1;

                    if (AJouer(c, l, joueur))
                    {
                        Console.WriteLine($"Votre nombre d'essais totale est de {essais++}");
                        if (Gagner(joueur))
                        {
                            gagner = true; // Rendre true gagner si on valide qu'un joueur est réussi à en aligner 3 ce qui mettre fin à la partie
                            AfficherMorpion();
                            Console.WriteLine($"Bravo au joueur n°{joueur} tu as gagné");
                        }
                        else if (VerifEgal()) // Sinon si faire égalité et mettre fin à la partie
                        {
                            AfficherMorpion();
                            Console.WriteLine("Il y a eu égalité !");
                            break; // Sortir de la boucle
                        }
                        else
                        {
                            joueur = joueur == 1 ? 2 : 1; // Changement de joueur
                        }
                    }
                }
                catch (Exception e) // Permet la gestion d'erreur de caractère saisie invalide
                {
                    Console.Clear();
                    Console.WriteLine("La saisie n'est pas valide. Veuillez rentrer un chiffre entre 1 et 3 !");
                }

            }; // Fin TQ

            // Fin de la partie
            Console.ReadKey();
        }
    }
}
