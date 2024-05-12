using System;

namespace ZooEnclosures
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal[] lions = { new Animal("Лев", "Мужской", "Рычание"), new Animal("Лев", "Женский", "Рычание") };
            Animal[] tigers = { new Animal("Тигр", "Мужской", "Рычание"), new Animal("Тигр", "Женский", "Рычание") };
            Animal[] bears = { new Animal("Медведь", "Мужской", "Рычание"), new Animal("Медведь", "Женский", "Рычание") };
            Animal[] wolves = { new Animal("Волк", "Мужской", "Вой"), new Animal("Волк", "Женский", "Вой") };

            Enclosure lionEnclosure = new Enclosure("Львиный вольер", lions);
            Enclosure tigerEnclosure = new Enclosure("Тигровый вольер", tigers);
            Enclosure bearEnclosure = new Enclosure("Медвежий вольер", bears);
            Enclosure wolfEnclosure = new Enclosure("Волчий вольер", wolves);

            Enclosure[] enclosures = { lionEnclosure, tigerEnclosure, bearEnclosure, wolfEnclosure };

            bool exit = false;

            while (!exit)
            {
                int initialChoice = 1;

                Console.WriteLine("Добро пожаловать в зоопарк! Выберите вольер для просмотра:");

                for (int i = 0; i < enclosures.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {enclosures[i].Name}");
                }

                Console.WriteLine("0. Выйти из программы");

                Console.Write("Введите номер вольера (или 0 для выхода): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;

                    default:
                        if (choice >= initialChoice && choice <= enclosures.Length)
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
    }

    class Animal
    {
        public string Species { get; set; }
        public string Gender { get; set; } 
        public string Sound { get; set; } 

        public Animal(string species, string gender, string sound)
        {
            Species = species;
            Gender = gender;
            Sound = sound;
        }
    }

    class Enclosure
    {
        public string Name { get; set; }
        public int NumberOfAnimals { get; set; }
        public Animal[] Animals { get; set; } 

        public Enclosure(string name, Animal[] animals)
        {
            Name = name;
            Animals = animals;
            NumberOfAnimals = animals.Length;
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
}
