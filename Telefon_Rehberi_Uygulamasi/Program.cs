using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Contact> phoneBook = new List<Contact>
        {
            new Contact { Name = "Ali Yılmaz", PhoneNumber = "1234567890" },
            new Contact { Name = "Ayşe Kaya", PhoneNumber = "2345678901" },
            new Contact { Name = "Mehmet Demir", PhoneNumber = "3456789012" },
            new Contact { Name = "Fatma Çelik", PhoneNumber = "4567890123" },
            new Contact { Name = "Ahmet Şahin", PhoneNumber = "5678901234" }
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Telefon Rehberi Uygulaması");
            Console.WriteLine("1. Telefon Numarası Kaydet");
            Console.WriteLine("2. Telefon Numarası Sil");
            Console.WriteLine("3. Telefon Numarası Güncelle");
            Console.WriteLine("4. Rehber Listeleme (A-Z, Z-A seçimli)");
            Console.WriteLine("5. Rehberde Arama");
            Console.WriteLine("6. Çıkış");
            Console.Write("Yapmak istediğiniz işlemi seçiniz: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddContact(phoneBook);
                    break;
                case "2":
                    DeleteContact(phoneBook);
                    break;
                case "3":
                    UpdateContact(phoneBook);
                    break;
                case "4":
                    ListContacts(phoneBook);
                    break;
                case "5":
                    SearchContacts(phoneBook);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddContact(List<Contact> phoneBook)
    {
        Console.Write("İsim: ");
        string name = Console.ReadLine();
        Console.Write("Telefon Numarası: ");
        string phoneNumber = Console.ReadLine();

        phoneBook.Add(new Contact { Name = name, PhoneNumber = phoneNumber });
        Console.WriteLine("Kişi rehbere eklendi.");
        Console.ReadKey();
    }

    static void DeleteContact(List<Contact> phoneBook)
    {
        Console.Write("Silinecek kişinin ismi: ");
        string name = Console.ReadLine();
        var contact = phoneBook.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (contact != null)
        {
            phoneBook.Remove(contact);
            Console.WriteLine("Kişi rehberden silindi.");
        }
        else
        {
            HandleNotFound("silme");
        }
        Console.ReadKey();
    }

    static void UpdateContact(List<Contact> phoneBook)
    {
        while (true)
        {
            Console.Write("Güncellenecek kişinin ismi: ");
            string name = Console.ReadLine();
            var contact = phoneBook.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                Console.Write("Yeni Telefon Numarası: ");
                contact.PhoneNumber = Console.ReadLine();
                Console.WriteLine("Kişi bilgileri güncellendi.");
                break;
            }
            else
            {
                int choice = HandleNotFound("güncelleme");
                if (choice == 1)
                {
                    break;
                }
            }
        }
        Console.ReadKey();
    }

    static void ListContacts(List<Contact> phoneBook)
    {
        Console.Write("Rehberi sıralama (1: A-Z, 2: Z-A): ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            var sortedContacts = phoneBook.OrderBy(c => c.Name).ToList();
            DisplayContacts(sortedContacts);
        }
        else if (choice == "2")
        {
            var sortedContacts = phoneBook.OrderByDescending(c => c.Name).ToList();
            DisplayContacts(sortedContacts);
        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");
        }
        Console.ReadKey();
    }

    static void SearchContacts(List<Contact> phoneBook)
    {
        Console.Write("Aranacak kişinin ismi: ");
        string name = Console.ReadLine();
        var foundContacts = phoneBook.Where(c => c.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

        if (foundContacts.Any())
        {
            DisplayContacts(foundContacts);
        }
        else
        {
            Console.WriteLine("Kişi bulunamadı.");
        }
        Console.ReadKey();
    }

    static void DisplayContacts(List<Contact> contacts)
    {
        foreach (var contact in contacts)
        {
            Console.WriteLine($"İsim: {contact.Name}, Telefon Numarası: {contact.PhoneNumber}");
        }
    }

    static int HandleNotFound(string action)
    {
        Console.WriteLine($"Aradığınız kriterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
        Console.WriteLine($"* {action} işlemini sonlandırmak için    : (1)");
        Console.WriteLine($"* Yeniden denemek için              : (2)");
        string choice = Console.ReadLine();
        return int.Parse(choice);
    }
}

class Contact
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}

