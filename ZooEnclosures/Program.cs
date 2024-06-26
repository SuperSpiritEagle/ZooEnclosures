﻿using System;
using System.Collections.Generic;

namespace ZooEnclosures
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal[] animalList =
            {
                new Animal("Лев", "Мужской", "Рычание"),
                new Animal("Лев", "Женский", "Рычание"),
                new Animal("Тигр", "Мужской", "Рычание"),
                new Animal("Тигр", "Женский", "Рычание"),
                new Animal("Медведь", "Мужской", "Рычание"),
                new Animal("Медведь", "Женский", "Рычание"),
                new Animal("Волк", "Мужской", "Вой"),
                new Animal("Волк", "Женский", "Вой")
            };

            Enclosure lionEnclosure = new Enclosure("Львиный вольер", AnimalHelper.GetAnimalsBySpecies("Лев", animalList));
            Enclosure tigerEnclosure = new Enclosure("Тигровый вольер", AnimalHelper.GetAnimalsBySpecies("Тигр", animalList));
            Enclosure bearEnclosure = new Enclosure("Медвежий вольер", AnimalHelper.GetAnimalsBySpecies("Медведь", animalList));
            Enclosure wolfEnclosure = new Enclosure("Волчий вольер", AnimalHelper.GetAnimalsBySpecies("Волк", animalList));

            Enclosure[] enclosures = { lionEnclosure, tigerEnclosure, bearEnclosure, wolfEnclosure };

            Zoo zoo = new Zoo(enclosures);

            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine("Добро пожаловать в зоопарк! Выберите вольер для просмотра:");

                for (int i = 0; i < enclosures.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {enclosures[i].Name}");
                }

                Console.WriteLine($"{MenuConstants.Exit}. Выйти из программы");

                int choice = ReadInt("Введите номер вольера (или 0 для выхода): ");

                switch (choice)
                {
                    case MenuConstants.Exit:
                        isExit = true;
                        break;

                    default:
                        if (choice >= MenuConstants.InitialChoice && choice <= enclosures.Length)
                        {
                            zoo.DisplayEnclosureInfo(choice - 1);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный номер вольера.");
                        }
                        break;
                }
            }

            Console.WriteLine("Спасибо за посещение зоопарка! До свидания.");
        }

        private static int ReadInt(string message)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                Console.Write(message);
            }

            return result;
        }
    }

    static class AnimalHelper
    {
        private static readonly Random random = new Random();

        public static Animal[] GetAnimalsBySpecies(string species, Animal[] animalList)
        {
            int count = random.Next(4, 9);
            List<Animal> selectedAnimals = new List<Animal>();

            foreach (var animal in animalList)
            {
                if (animal.Species == species)
                {
                    selectedAnimals.Add(animal.Clone());
                    selectedAnimals.Add(animal.CloneWithDifferentGender());

                    if (selectedAnimals.Count >= count)
                    {
                        break;
                    }
                }
            }

            return selectedAnimals.ToArray();
        }
    }

    class Animal
    {
        public Animal(string species, string gender, string sound)
        {
            Species = species;
            Gender = gender;
            Sound = sound;
        }

        public string Species { get; }
        public string Gender { get; }
        public string Sound { get; }

        public Animal Clone()
        {
            return new Animal(Species, Gender, Sound);
        }

        public Animal CloneWithDifferentGender()
        {
            string newGender = Gender == "Мужской" ? "Женский" : "Мужской";
            return new Animal(Species, newGender, Sound);
        }
    }

    class Enclosure
    {
        private Animal[] _animals;

        public Enclosure(string name, Animal[] animals)
        {
            Name = name;
            _animals = animals;
        }

        public string Name { get; }
        public int NumberOfAnimals => _animals.Length;

        public void DisplayInfo()
        {
            Console.WriteLine($"Вольер: {Name}");
            Console.WriteLine($"Количество животных: {NumberOfAnimals}");
            Console.WriteLine("Животные:");

            foreach (var animal in _animals)
            {
                Console.WriteLine($"- Вид: {animal.Species}, Пол: {animal.Gender}, Звук: {animal.Sound}");
            }
        }
    }

    class Zoo
    {
        private Enclosure[] _enclosures;

        public Zoo(Enclosure[] enclosures)
        {
            _enclosures = enclosures;
        }

        public void DisplayEnclosureInfo(int index)
        {
            _enclosures[index].DisplayInfo();
        }
    }

    static class MenuConstants
    {
        public const int Exit = 0;
        public const int InitialChoice = 1;
    }
}
