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

            Enclosure lionEnclosure = new Enclosure("Львиный вольер", GetAnimalsBySpecies("Лев", animalList));
            Enclosure tigerEnclosure = new Enclosure("Тигровый вольер", GetAnimalsBySpecies("Тигр", animalList));
            Enclosure bearEnclosure = new Enclosure("Медвежий вольер", GetAnimalsBySpecies("Медведь", animalList));
            Enclosure wolfEnclosure = new Enclosure("Волчий вольер", GetAnimalsBySpecies("Волк", animalList));

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

                Console.WriteLine($"{MenuConstants.Exit}. Выйти из программы");

                Console.Write("Введите номер вольера (или 0 для выхода): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                    continue;
                }

                switch (choice)
                {
                    case MenuConstants.Exit:
                        exit = true;
                        break;

                    default:
                        if (choice >= MenuConstants.InitialChoice && choice <= enclosures.Length)
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

        static Animal[] GetAnimalsBySpecies(string species, Animal[] animalList)
        {
            Random random = new Random();
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
        private string species;
        private string gender;
        private string sound;

        public string Species => species;
        public string Gender => gender;
        public string Sound => sound;

        public Animal(string species, string gender, string sound)
        {
            this.species = species;
            this.gender = gender;
            this.sound = sound;
        }

        public Animal Clone()
        {
            return new Animal(this.species, this.gender, this.sound);
        }
    }

    class Enclosure
    {
        private string name;
        private int numberOfAnimals;
        private Animal[] animals;

        public string Name => name;
        public int NumberOfAnimals => numberOfAnimals;
        public Animal[] Animals => animals;

        public Enclosure(string name, Animal[] animals)
        {
            this.name = name;
            this.animals = animals;
            this.numberOfAnimals = animals.Length;
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
        private Enclosure[] enclosures;

        public Zoo(Enclosure[] enclosures)
        {
            this.enclosures = enclosures;
        }

        public Enclosure[] Enclosures => enclosures;
    }

    static class MenuConstants
    {
        public const int Exit = 0;
        public const int InitialChoice = 1;
    }
}
