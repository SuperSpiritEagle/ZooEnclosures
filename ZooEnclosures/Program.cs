using System;

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
        public static Animal[] GetAnimalsBySpecies(string species, Animal[] animalList)
        {
            int count = 2;
            Animal[] selectedAnimals = new Animal[count];

            int index = 0;

            foreach (var animal in animalList)
            {
                if (animal.Species == species)
                {
                    selectedAnimals[index++] = animal.Clone();

                    if (index == count)
                    {
                        break;
                    }
                }
            }

            return selectedAnimals;
        }
    }

    class Animal
    {
        public string Species { get; }
        public string Gender { get; }
        public string Sound { get; }

        public Animal(string species, string gender, string sound)
        {
            Species = species;
            Gender = gender;
            Sound = sound;
        }

        public Animal Clone()
        {
            return new Animal(Species, Gender, Sound);
        }
    }

    class Enclosure
    {
        public string Name { get; }
        public int NumberOfAnimals => Animals.Length;
        public Animal[] Animals { get; }

        public Enclosure(string name, Animal[] animals)
        {
            Name = name;
            Animals = animals;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Вольер: {Name}");
            Console.WriteLine($"Количество животных: {NumberOfAnimals}");
            Console.WriteLine("Животные:");

            foreach (var animal in Animals)
            {
                Console.WriteLine($"- Вид: {animal.Species}, Пол: {animal.Gender}, Звук: {animal.Sound}");
            }
        }
    }

    class Zoo
    {
        public Enclosure[] Enclosures { get; }

        public Zoo(Enclosure[] enclosures)
        {
            Enclosures = enclosures;
        }

        public void DisplayEnclosureInfo(int index)
        {
            Enclosures[index].DisplayInfo();
        }
    }

    static class MenuConstants
    {
        public const int Exit = 0;
        public const int InitialChoice = 1;
    }
}
