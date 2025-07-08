using System;
using System.Collections.Generic;
using System.Linq;
using static VeriYapıları.BST;
using VeriYapıları;
using System.Security.Cryptography.X509Certificates;

namespace KoyKurtarmaOyunu
{
    class Program
    {   static Stack <string> canta=new Stack<string>();
        static Queue<string> kurtarilacakKoyler = new Queue<string>();
        static string[] koyler = new string[7];
        static BST bst = new BST();
        static List<string> kurtarilanKoyler = new List<string>();

        static Dictionary<string, List<string>> ozelKoyGereksinimleri = new Dictionary<string, List<string>>()
        {   { "Orman Köyü", new List<string> { "balta" } },
            { "Deniz Köyü", new List<string> { "bot" } },
            { "Dağ Köyü", new List<string> { "tırmanma ipi" } },
            { "Göl Köyü", new List<string> { "sandal" } },
            { "Büyücü Köyü", new List<string> { "büyü taşı", "iksir" } },
            { "Devler Köyü", new List<string> { "kalkan", "dev kılıç" } },
            { "Ejderhalar Köyü", new List<string> { "buz topu","ayna" } }
        };

        static void Main(string[] args)
        {
            KoyleriYukle();
            var esya = new Item("balta");
            
            canta.Push("balta");
            bst.Insert(esya);
            Console.WriteLine("Oyuna başlarken size bir balta verildi!");
            Menu();
        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("--- MENÜ ---");
                
                Console.WriteLine("1. Eşya Ara");
                Console.WriteLine("2. Çantayı Göster");
                Console.WriteLine("3. Köy Kurtar");
                Console.WriteLine("4. Köyleri Listele");
                Console.WriteLine("5. Eşya Kullan");
                Console.WriteLine("6. Kurtarılmayı Bekleyen Köyler");
                Console.WriteLine("7. Kurtarılan Köyler");

                Console.WriteLine("0. Çıkış");

        
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {

                    case "1": EsyaAra(); 
                        break;
                    case "2": CantayiGoster(); 
                        break;
                    case "3": sıradakiKoyuKurtar(); 
                        break;
                    case "4": KoyleriListele(); 
                        
                        break;
                    case "5":
                        useItem();
                        break;
                    case "6": kurtarilacakKoyListesi();
                        
                        break;
                    case "7": kurtarilanKoylerListesi();
                        break;

                    case "0": return;
                    default: Console.WriteLine("Geçersiz seçim."); break;
                }
            }
        }
        static void KoyleriYukle()
        {
            koyler[0] = "Orman Köyü";
            koyler[1] = "Deniz Köyü";
            koyler[2] = "Dağ Köyü";
            koyler[3] = "Göl Köyü";
            koyler[4] = "Büyücü Köyü";
            koyler[5] = "Devler Köyü";
            koyler[6] = "Ejderhalar Köyü";
            foreach (var x in koyler)
                kurtarilacakKoyler.Enqueue(x);
        }
        static void EsyaAra()
        {
            Console.Write("Aranacak eşya: ");
            string name = Console.ReadLine().ToLower();
            Item found = bst.Search(name);
            if (found != null)
                Console.WriteLine($"Bulundu: {found}");
            else
                Console.WriteLine("Eşya bulunamadı.");
        }

        static void CantayiGoster()
        {
            Console.WriteLine("Çantadaki eşyalar:");
            if (canta.Count == 0)
            {
                Console.WriteLine("Çanta boş.");
                return;
            }

            foreach (var esya in canta)
            {
                Console.WriteLine(esya);
            }
        }

        static void KoyleriListele()
        {
            Console.WriteLine("Köyler:");
            for (int i = 0; i < koyler.Length; i++)
            {
                string koy = koyler[i];
                Console.WriteLine($"{i + 1}. {koyler[i]}");
                if (koyEnvanteri.ContainsKey(koy))
                {
                    var esyalar = koyEnvanteri[koy];
                    
                    Console.WriteLine($"   Bu köyden kazanılan eşyalar: {string.Join(", ", esyalar)}");
                }

                // Bir sonraki köyün gereksinimlerini göster
                if (i + 1 < koyler.Length)
                {
                    string sonrakiKoy = koyler[i + 1];
                    if (ozelKoyGereksinimleri.ContainsKey(sonrakiKoy))
                    {
                        var gerekenler = ozelKoyGereksinimleri[sonrakiKoy];
                        Console.WriteLine($"   -> Bir sonraki köy ({sonrakiKoy}) için gerekli eşyalar: {string.Join(", ", gerekenler)}");
                    }
                }

            }
        }
        static Dictionary<string, List<string>> koyEnvanteri = new Dictionary<string, List<string>>()
{
    { "Orman Köyü", new List<string> { "balta", "dev kılıç","bot" } },
    { "Deniz Köyü", new List<string> { "ayna", "iksir","tırmanma ipi" } },
    { "Dağ Köyü", new List<string> { "büyü taşı", "sandal","sandal" } },
    { "Göl Köyü", new List<string> { "kalkan", "buz topu","büyü taşı","iksir" } },
    { "Büyücü Köyü", new List<string> { "büyü taşı", "ayna","kalkan","dev kılıç" } },
    { "Devler Köyü", new List<string> { "iksir", "bot","buz topu" ,"Ayna"} },
    { "Ejderhalar Köyü", new List<string> { "dev kılıç", "buz topu","Başarı Rozeti" } }
};

        static void kurtarilacakKoyListesi()
        {
            Console.WriteLine("Kurtarılacak köyler:");
            if (kurtarilacakKoyler.Count == 0)
            {
                Console.WriteLine("Tüm köyler kurtarıldı!");
                return;
            }
            foreach (var koy in kurtarilacakKoyler)
            {
                Console.WriteLine(koy);
            }
        }
        static void kurtarilanKoylerListesi()
        {
            if (kurtarilanKoyler.Count == 0)
            {
                Console.WriteLine("Kurtarılan köy yok!");
                return;
            }
            foreach (var koy in kurtarilanKoyler)
            {
                Console.WriteLine(koy);
            }
        }
        static List<string> KazanilanEsya(string mevcutKoy)
        {
            int index = Array.IndexOf(koyler, mevcutKoy); //Mevcut köyün koyler dizisindeki index'ini bulur.
            if (index + 1 < koyler.Length)
            {
                string sonrakiKoy = koyler[index + 1];
                if (ozelKoyGereksinimleri.ContainsKey(sonrakiKoy))
                    return new List<string>(ozelKoyGereksinimleri[sonrakiKoy]); 
            }
            return new List<string>();
        }


        static void sıradakiKoyuKurtar()
        {
            if (kurtarilacakKoyler.Count == 0)
            {
                Console.WriteLine("Tüm Köyleri Kurtardın Tebrikler Savaşçı");

            }
            else
            {

                string sıradakiKoy = kurtarilacakKoyler.Peek();
                var kullanilacakEsya = ozelKoyGereksinimleri[sıradakiKoy];
                if (kullanilacakEsya.All(x => canta.Contains(x)))
                {
                    kurtarilacakKoyler.Dequeue();
                    kurtarilanKoyler.Add(sıradakiKoy);


                    foreach (var x in kullanilacakEsya)
                    {
                        canta.Pop();
                        Console.WriteLine($"'{x}' çantadan kullanıldı.");
                    }
                    Console.WriteLine($"{sıradakiKoy} başarıyla kurtarıldı!");

                    List<string> kazanilanEsyalar = KazanilanEsya(sıradakiKoy);


                    switch (sıradakiKoy)
                    {
                        case "Orman Köyü":
                            kazanilanEsyalar.AddRange(new[] { "balta", "dev kılıç" });
                            break;
                        case "Deniz Köyü":
                            kazanilanEsyalar.AddRange(new[] { "ayna", "iksir" });
                            break;
                        case "Dağ Köyü":
                            kazanilanEsyalar.AddRange(new[] { "büyü taşı","sandal" });
                            break;
                        case "Göl Köyü":
                            kazanilanEsyalar.AddRange(new[] { "kalkan" ,"buz topu"});
                            break;
                        case "Büyücü Köyü":
                            kazanilanEsyalar.AddRange(new[] { "büyü taşı","ayna" });
                            break;
                        case "Devler Köyü":
                            kazanilanEsyalar.AddRange(new[] { "iksir","bot" });
                            break;
                        case "Ejderhalar Köyü":
                            kazanilanEsyalar.AddRange(new[] { "dev kılıç","buz topu","Başarı Rozeti" });
                            break;
                    }

                


                    foreach (var esya in kazanilanEsyalar)
                    {
                        if (canta.Count >= 10)
                        {
                            Console.WriteLine($"'{esya}' eşyasını alamadın. Çanta dolu!Eşya Çıkarmak İster Misin?");
                            string soru = Console.ReadLine().ToLower();
                            if (soru == "evet")
                            {
                                useItem();
                                canta.Push(esya);
                            }
                            else
                            {
                                Console.WriteLine($"'{esya}' eşyası alınamadı.");
                            }
                        }


                        else
                        {
                            canta.Push(esya);
                            bst.Insert(new Item(esya));

                            Console.WriteLine($"'{esya}' çantaya eklendi.");
                        }

                    }
                }
                else
                {
                    Console.WriteLine($"{sıradakiKoy} kurtarılamadı. Eksik eşyalar:");
                    foreach (var esya in kullanilacakEsya.Where(e => !canta.Contains(e)))
                    {
                        Console.WriteLine("- " + esya);
                    }
                }


            }
        }

        static void useItem()
        {
            if (canta.Count!=0)
            {
                string kullanilan = canta.Pop();
                bst.Delete(kullanilan);
                Console.WriteLine($"'{kullanilan}' çantadan çıkarıldı ve ağaçtan silindi.");
            }
            else
            {
                Console.WriteLine("Çanta Boş");
            }
        }


    }
}



   
