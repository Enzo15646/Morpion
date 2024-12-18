﻿using System;
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
                    if (grille[i, j] == 1)
                        Console.Write(" X ");
                    else if (grille[i, j] == 2)
                        Console.Write(" O ");
                    else
                        Console.Write(" . ");

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
            if (l < 0 || l >= 3 || c < 0 || c >= 3)
            {
                Console.WriteLine("Vous etes en dehors des limites de la grille !");
                return false;
            }
            if (grille[c, l] != 10)
            {
                Console.WriteLine("La case séléctionné est déjà occupée !");
                return false;
            }
            Console.Clear();
            grille[c, l] = joueur;
            return true;

        }
        // Fonction permettant de vérifier
        // si un joueur à gagner
        public static bool Gagner(int joueur)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((grille[i, 0] == joueur && grille[i, 1] == joueur && grille[i, 2] == joueur) || (grille[0, i] == joueur && grille[1, i] == joueur && grille[2, i] == joueur)) // Vérification horizontale
                {
                    return true;
                }
            }
            if ((grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur) || (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur)) // Vérification verticale
            {
                return true;
            }
            return false;
        }
         
        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations ---

            int essais = 1;    // compteur d'essais
	        int joueur = 1 ;   // 1 pour la premier joueur, 2 pour le second
	        int l, c = 0;      // numéro de ligne et de colonne
            int i, j = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10
	        for (j=0; j < grille.GetLength(0); j++)
		        for (i=0; i < grille.GetLength(1); i++)
			        grille[j,i] = 10;
            while(!gagner && essais != 9)
            {
                AfficherMorpion();
                Console.WriteLine($"Joueur n°{joueur}, c'est à vous de jouer !"); // Message pour annoncer quel joueur doit jouer
                try
                {
                    Console.Write("Ligne (1-3) : ");
                    l = int.Parse(Console.ReadLine()) - 1;

                    Console.Write("Colonne (1-3) : ");
                    c = int.Parse(Console.ReadLine()) - 1;

                    if (AJouer(c, l, joueur))
                    {
                        Console.WriteLine($"Votre nombre d'essais totale est de {essais++}");
                        if (Gagner(joueur))
                        {
                            gagner = true;
                            AfficherMorpion();
                            Console.WriteLine($"Bravo au joueur n°{joueur} tu as gagné");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                // Changement de joueur
                // A compléter 

            }; // Fin TQ

            // Fin de la partie
            Console.ReadKey();
    }
  }
}
