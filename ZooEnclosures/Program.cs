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

            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("Добро пожаловать в зоопарк! Выберите вольер для просмотра:");

                for (int i = 0; i < enclosures.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {enclosures[i].Name}");
                }

                Console.WriteLine($"{MenuConstants.EXIT}. Выйти из программы");

                int choice = ReadInt("Введите номер вольера (или 0 для выхода): ");

                switch (choice)
                {
                    case MenuConstants.EXIT:
                        exit = true;
                        break;

                    default:
                        if (choice >= MenuConstants.INITIAL_CHOICE && choice <= enclosures.Length)
                        {
                            enclosures[choice - 1].DisplayInfo();
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
            while (!int.TryParse(Console.ReadLine(), out result))
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
            int count = 2; // Number of animals per enclosure
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
        private readonly string _species;
        private readonly string _gender;
        private readonly string _sound;

        public string Species => _species;
        public string Gender => _gender;
        public string Sound => _sound;

        public Animal(string species, string gender, string sound)
        {
            _species = species;
            _gender = gender;
            _sound = sound;
        }

        public Animal Clone()
        {
            return new Animal(_species, _gender, _sound);
        }
    }

    class Enclosure
    {
        private readonly string _name;
        private readonly Animal[] _animals;

        public string Name => _name;
        public int NumberOfAnimals => _animals.Length;

        public Enclosure(string name, Animal[] animals)
        {
            _name = name;
            _animals = animals;
        }

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
        private readonly Enclosure[] _enclosures;

        public Zoo(Enclosure[] enclosures)
        {
            _enclosures = enclosures;
        }

        public Enclosure[] GetEnclosures()
        {
            return (Enclosure[])_enclosures.Clone();
        }
    }

    static class MenuConstants
    {
        public const int EXIT = 0;
        public const int INITIAL_CHOICE = 1;
    }
}
