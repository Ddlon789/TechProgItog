using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using System.Xml;

public class Program
{
    public static void Main(string[] args)
    {
        // Pohod.rasbor_po_kucham();
        Live.period();
    }
}

public class Sborka
{
    public static Random random = new Random();

    public static string genType()
    {
        string alp_1 = "aeioyu";
        string alp_2 = "bcdfghjklmnpqrstxwzv";
        string result = "";
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0) result += alp_2[random.Next(alp_2.Length)];
            else result += alp_1[random.Next(alp_1.Length)];
        }

        return result;
    }

    public static List<Kolonia> genKolonys()
    {
        int size_kolonias = random.Next(6, 10);
        List<Kolonia> kolonias = new List<Kolonia>(){};
        for (int i = 0; i < size_kolonias; i++)
        {
            kolonias.Add(genInsect());
        }
        return kolonias;
    }

    public static Ant genAntrab(King king)
    {
        List<string> types_rab = new() {"обычный", "старший", "продвинутый марафонец"};
        int l = random.Next(0, types_rab.Count - 1);
        string type = null;
        if (l == 0)
        { 
            type = types_rab[0];
        }

        if (l == 1)
        {
            type = types_rab[1];
        }

        if (l == 2)
        {
            type = types_rab[2];
        }

        Ant rab_ant = new Ant(type, king.name_k, king, Proffessy.WORKER, king.name_kol, king.kids_ant, king.kids_kings,
            null);
        return rab_ant;
    }

    // public static Ant genAntvoin(King king)
    // {
    //     List<string> types_voin = new() {"продвинутый", "легендарный", "легендарный феникс"};
    //     int l = random.Next(0, types_voin.Count - 1);
    //     string type = null;
    //     if (l == 0)
    //     { 
    //         type = types_voin[0];
    //     }
    //
    //     if (l == 1)
    //     {
    //         type = types_voin[1];
    //     }
    //
    //     if (l == 2)
    //     {
    //         type = types_voin[2];
    //     }
    //
    //     Ant voin_ant = new Ant(type, king.name_k, king, Proffessy.ARMY, king.name_kol, king.kids_ant, king.kids_kings,
    //         null);
    //     return voin_ant;
    // }
    //
    // public static List<Ant> genAnts_1(King king)
    // {
    //     List<Ant> ants = new();
    //     int rab = 17;
    //     int voin = 8;
    //     int spec = 1;
    //
    //     for (int i = 0; i < rab; i++)
    //     {
    //         ants.Add(genAntrab(king));
    //     }
    //
    //     for (int i = 0; i < voin; i++)
    //     {
    //         ants.Add(genAntvoin(king));
    //     }
    //
    //     Ant spec_ant = new Ant("Медведка", king.name_k, king, Proffessy.SPECIAL, king.name_kol, king.kids_ant,
    //         king.kids_kings, null);
    //
    //     ants.Add(spec_ant);
    //
    //     return ants;
    // }
    //
    // public static List<>

    public static Kolonia genInsect()
    {
        string name_kol = genType();
        
        int sizeWorkAnt = random.Next(7, 15);
        int sizeWarAnts = random.Next(7, 15);
        int sizeSpecialIns = random.Next(7, 15);
        int k = random.Next(6, 21);
        int l = random.Next(6, 21);
        int v = random.Next(6, 21);
        int r = random.Next(6, 21);
        int kol_vo_ants = random.Next(16, 32);
        int kol_vo_new_king = random.Next(1, 4);
        //
        // List<WorkAnt> WorkAnts = new List<WorkAnt>() {};
        // List<WarAnt> WarAnts = new List<WarAnt>() {};
        // List<SpecialInc> specialIncs = new List<SpecialInc>() {};
        List<Ant> Ants = new List<Ant>() { };
        List<King> new_Kings = new List<King>() { };
        King king = genKings(kol_vo_ants, kol_vo_new_king, name_kol);

        // for (int i = 0; i < sizeWorkAnt; i++)
        // {
        //     WorkAnts.Add(genWorkAnt(king));
        // }
        //
        // for (int i = 0; i < sizeWarAnts; i++)
        // {
        //     WarAnts.Add(genWarAnt(king));
        // }
        //
        // for (int i = 0; i < sizeSpecialIns; i++)
        // {
        //     specialIncs.Add(genSpecialInc(king));
        // }

        for (int i = 0; i < kol_vo_ants; i++)
        {
            Ants.Add(genAnt(king, name_kol));
        }

        for (int i = 0; i < kol_vo_new_king; i++)
        {
            new_Kings.Add(genKings(0, 0, name_kol));
        }

        Kolonia kolonia = new Kolonia(name_kol, Ants, king, k, l, v, r, new_Kings);
        return kolonia;
    }

    public static Ant genAnt(King king, string name_kol)
    {
        string name_a = genType();
        int df = random.Next(0, 3);
        Proffessy proffessy = Proffessy.ARMY;
        if (df == 0)
        { 
            proffessy = Proffessy.ARMY;
        }
        else if(df == 1)
        {
            proffessy = Proffessy.WORKER;
        }
        else if (df == 2)
        { 
            proffessy = Proffessy.SPECIAL;
        }

        Ant ant = new Ant(name_a, king.name_k, king, proffessy, name_kol, king.kids_ant, king.kids_kings, null);
        return ant;
    }

    

    public static King genKings(int kid_ants, int kid_kings, string name_kol)
    {
        string name_k = genType();

        King king = new King(name_k, kid_ants, kid_kings, name_kol, null);
        return king;
    }

    // public static WarAnt genWarAnt(King king)
    // {
    //     string name_a = genType();
    //     string name_k = king.name_k;
    //     int kids = king.kids;
    //     King king1 = king;
    //     WarAnt warAnt = new WarAnt(name_a, name_k, kids, king, Proffessy.ARMY);
    //     return warAnt;
    // }
    //
    // public static WorkAnt genWorkAnt(King king)
    // {
    //     string name_a = genType();
    //     string name_k = king.name_k;
    //     int kids = king.kids;
    //     King king1 = king;
    //     WorkAnt workAnt = new WorkAnt(name_a, name_k, kids, king, Proffessy.WORKER);
    //     return workAnt;
    // }
    //
    // public static SpecialInc genSpecialInc(King king)
    // {
    //     string name_a = genType();
    //     string name_k = king.name_k;
    //     int kids = king.kids;
    //     King king1 = king;
    //     SpecialInc specialInc = new SpecialInc(name_a, name_k, kids, king, Proffessy.SPECIAL);
    //     return specialInc;
    // }

    public static List<Kucha> genKuchas()
    {
        int size_kuchas = random.Next(5, 12);
        List<Kucha> kuchas = new List<Kucha>(){};
        for (int i = 0; i < size_kuchas; i++)
        {
            kuchas.Add(genKucha());
        }

        return kuchas;
    }

    public static Kucha genKucha()
    {
        int k = random.Next(6, 21);
        int l = random.Next(6, 21);
        int v = random.Next(6, 21);
        int r = random.Next(6, 21);

        Kucha kucha = new Kucha(k, l, v, r);
        return kucha;
    }
}

public class Live
{
    public static Random random = new Random();

    public static Dictionary<List<Ant>, List<King>> gen_lichinki(King king)
    {
        int kol_vo_ant = random.Next(2, 4);
        List<Ant> lichinki = new();

        for (int i = 0; i < kol_vo_ant; i++)
        {
            lichinki.Add(Sborka.genAnt(king, king.name_kol));
        }
        
        int kol_vo_king = random.Next(1, 3);
        List<King> kings = new();

        for (int i = 0; i < kol_vo_king; i++)
        {
            kings.Add(Sborka.genKings(kol_vo_ant, kol_vo_king, king.name_kol));
        }

        Dictionary<List<Ant>, List<King>> dict = new();
        dict.Add(lichinki, kings);

        return dict;
    }

    public static void prisvaivanie_lichinok( ref King king)
    {
        Dictionary<List<Ant>, List<King>> dict = gen_lichinki(king);
        var item = dict.ElementAt(0);
        var lichinki = item.Key;
        var kings = item.Value;

        king.kids_ant = lichinki.Count;
        king.kids_kings = kings.Count;

        king.lichinki_ant = lichinki;
        king.lichinki_king = kings;
    }

    public static void upakovka(ref Kolonia kolonia)
    {
        for (int i = 0; i < kolonia.king.lichinki_ant.Count; i++)
        {
            kolonia.Ants.Add(kolonia.king.lichinki_ant[i]);   
        }

        for (int i = 0; i < kolonia.king.lichinki_king.Count; i++)
        {
            kolonia.new_Kings.Add(kolonia.king.lichinki_king[i]);
        }
    }

    public static Kolonia genNewKolony(King king)
    {
        
        string name_kol = Sborka.genType();
        int k = random.Next(6, 21);
        int l = random.Next(6, 21);
        int v = random.Next(6, 21);
        int r = random.Next(6, 21);
        int kol_vo_ants = random.Next(16, 32);
        List<Ant> Ants = new List<Ant>() { };
        List<King> new_Kings = new List<King>() { };
        
        for (int i = 0; i < kol_vo_ants; i++)
        {
            Ants.Add(Sborka.genAnt(king, name_kol));
        }

        Kolonia kolonia = new Kolonia(name_kol, Ants, king, k, l, v, r, new_Kings);
        return kolonia;
    }
    public static void period()
    {
        List<Kucha> kuchas = Sborka.genKuchas();
        List<Kolonia> kolonias = Sborka.genKolonys();
        int zasuha = 15;
        int days = 40;
        int day = 1;
        int rost = 4;
        int pohod = 1;
        int live = 10;

        Console.WriteLine("НАЧАЛО ХОДА:");
        for (int i = 0; i < zasuha; i++)
        {
            int do_zasuhi = zasuha - i;
            Console.WriteLine($"День: {i+1} (Количество дней до засухи: {do_zasuhi} ");
                for (int j = 0; j < kolonias.Count; j++)
                {
                    Kolonia kolonia = kolonias[j];
                    var king_kolony = kolonia.king;
                    prisvaivanie_lichinok(ref king_kolony);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Колония <<{kolonia.name_kol}>> {j}:");
                    Console.WriteLine($"---Королева <<{king_kolony.name_k}>>, Личинок: {king_kolony.kids_ant + king_kolony.kids_kings}");
                    Console.WriteLine($"---Ресурсы: k:{kolonia.k} l: {kolonia.l} v: {kolonia.v} r: {kolonia.r}");
                    Console.WriteLine($"---Популяция {kolonia.popul_full()}: рабочие: {kolonia.popul_work()} военные: {kolonia.popul_war()} особые: {kolonia.popul_spec()}");
                    Console.WriteLine();
                    Console.WriteLine();
                    // Console.WriteLine($"Если хотите вывести информацию по каждому насекомому введите - 1");
                    // Console.WriteLine("Eсли хотите вывести более общую информацию по колонии нажмите - 2");
                    // Console.WriteLine("Если хотите пропустить нажмите любую клавишу кроме 1 и 2");
                    // Console.Write(":= ");
                    // string s = Console.ReadLine();
                    // if (s == "1")
                    // {
                    //     Console.WriteLine($"Королева {kolonias[j].king.name_k}");
                    //     Console.WriteLine($"---Параметры: ");
                    //     foreach (var ant in kolonias[j].Ants)
                    //     {
                    //         if (ant.Proffessy == Proffessy.ARMY)
                    //         {
                    //             ant.health = 100;
                    //         }
                    //         ant.TalkAboutYou();
                    //         Console.WriteLine();
                    //         Console.WriteLine();
                    //     }
                    // }

                    upakovka(ref kolonia);

                    for (int k = 0; k < kolonia.new_Kings.Count; k++)
                    {
                        int deisvie_dlya_king = random.Next(0, 15);
                        kolonia.new_Kings[k].name_mom_k = kolonia.king.name_k;
                        if (deisvie_dlya_king < 15)
                        {
                            kolonia.new_Kings.Remove(kolonia.new_Kings[k]);
                        }

                        if (deisvie_dlya_king == 15)
                        {
                            Kolonia kolonia_new = genNewKolony(kolonia.new_Kings[k]);
                            kolonias.Add(kolonia_new);
                        }
                    }
                }
                
                

                for (int j = 0; j < kuchas.Count; j++)
                {
                    if (kuchas[j].k == 0 && kuchas[j].l == 0 && kuchas[j].v == 0 && kuchas[j].r == 0)
                    {
                        Console.WriteLine($"Куча {j}: истощена");
                    }
                    else
                    {
                        Console.WriteLine($"Куча {j}: k: {kuchas[j].k} l: {kuchas[j].l} v: {kuchas[j].v} r: {kuchas[j].r}");   
                    }
                }
                Console.WriteLine();
                Console.WriteLine();

                if (do_zasuhi < 4)
                {
                    Console.WriteLine($"{i} день - ДЕНЬ ПОХОДА");
                    Console.WriteLine("Начало дня:");
                    Pohod.rasbor_po_kucham(ref kuchas, ref kolonias);
                    
                    // for (int j = 0; j < kuchas.Count; j++)
                    // {
                    //     if (kuchas[j].k == 0 && kuchas[j].l == 0 && kuchas[j].v == 0 && kuchas[j].r == 0)
                    //     {
                    //         kuchas.RemoveAt(j);
                    //     }
                    // }
                    
                }
        }

        Console.WriteLine("Засуха");
    }
}
public class Pohod
{
    public static Random random = new Random();

    public static void rasbor_po_kucham(ref List<Kucha> kuchas,  ref List<Kolonia> kolonias)
    {
        // List<Kucha> kuchas = Sborka.genKuchas();
        // List<Kolonia> kolonias = Sborka.genKolonys();

        int[] scores = new int[kolonias.Count * 4];

        List<int> hoi = new List<int>();

        var pohodiNaKuchi = new Dictionary<int, int>() { };

        for (int i = 0; i < kolonias.Count; i++)
        {
            int int_kucha = random.Next(0, kuchas.Count);
            pohodiNaKuchi.Add(i, int_kucha);
        }

        for (int i = 0; i < pohodiNaKuchi.Count; i++)
        {
            var item = pohodiNaKuchi.ElementAt(i);
            var itemkey = item.Key;
            var itemvalue = item.Value;
            scores[itemvalue] += 1;
        }

        foreach (var hj in pohodiNaKuchi)
        {
            Console.WriteLine($"С начала дня Колония:{hj.Key} отправилась на кучу: {hj.Value} В составе: рабочие: {kolonias[hj.Key].popul_work()} военные: {kolonias[hj.Key].popul_war()} особые: {kolonias[hj.Key].popul_spec()}");
        }
        
        Console.WriteLine("----------------------------------------");

        IDictionary<int, int> types = pohodiNaKuchi;

        Dictionary<int, List<int>> slovar_po_kucham = new Dictionary<int, List<int>>() { };
        // Dictionary<int, List<int>> slovar_po_kucham_1 = new Dictionary<int, List<int>>() { };

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] >= 1)
            {
                List<int> mass_war = new List<int>() { };
                for (int j = 0; j < scores[i]; j++)
                {
                    int posel = types.FirstOrDefault(x => x.Value == i).Key;
                    mass_war.Add(posel);
                    types.Remove(posel);
                }

                slovar_po_kucham.Add(i, mass_war);

                // Console.WriteLine("----------------------------------");
                // Console.WriteLine($"куча {i}:");
                // foreach (var war in mass_war)
                // {
                //     Console.WriteLine($"{war} ");
                // }
                //
                // Console.WriteLine("-----------------------------------");
            }
            // if (scores[i] == 1)
            // {
            //     List<int> mass_war_1 = new List<int>() { };
            //     for (int j = 0; j < scores[i]; j++)
            //     {
            //         int posel_1 = types.FirstOrDefault(x => x.Value == i).Key;
            //         mass_war_1.Add(posel_1);
            //         types.Remove(posel_1);
            //     }
            //
            //     slovar_po_kucham_1.Add(i, mass_war_1);

            // Console.WriteLine("---------------Kuchi 1-------------------");
            // Console.WriteLine($"куча {i}:");
            // foreach (var war1 in mass_war_1)
            // {
            //     Console.WriteLine($"{war1} ");
            // }

            // foreach (var gh in slovar_po_kucham_1)
            // {
            //     Console.WriteLine($"Kucha: {gh.Key} Kolonia {gh.Value[0]}");
            // }
            // }
        }

        Console.WriteLine();
        Console.WriteLine("Конец дня");
        if (slovar_po_kucham.Count > 0)
        {
            War(ref slovar_po_kucham,ref kuchas, ref kolonias);
        }
    }

    public static void War(ref Dictionary<int, List<int>> kuchi_kolonii,ref List<Kucha> kuchas,ref List<Kolonia> kolonias)
    {
        int count = 0;
        for (int i = 0; i < kuchi_kolonii.Count; i++)
        {
            var item = kuchi_kolonii.ElementAt(i);
            int nomer_kuchi = item.Key;
            var mass_kolonys = item.Value;

            if (mass_kolonys.Count >= 2)
            {
                List<Ant> all_ant = new List<Ant>();
                for (int j = 0; j < mass_kolonys.Count; j++)
                {
                    for (int k = 0; k < kolonias[mass_kolonys[j]].Ants.Count; k++)
                    {
                        all_ant.Add(kolonias[mass_kolonys[j]].Ants[k]);
                    }
                }

                for (int l = 0; l < all_ant.Count; l++)
                {
                    int jk = random.Next(0, all_ant.Count);
                    if (jk == l || all_ant[l].name_kol == all_ant[jk].name_kol)
                    {
                        int loop = 0;
                        while (jk == l || all_ant[l].name_kol == all_ant[jk].name_kol)
                        {
                            jk = random.Next(0, all_ant.Count);
                            loop += 1;
                            if (loop > 10000)
                            {
                                break;
                            }
                        }
                    }

                    if (jk == l || all_ant[l].name_kol == all_ant[jk].name_kol)
                    {
                        continue;
                    }

                    if (all_ant[l].king.name_k == all_ant[jk].king.name_mom_k)
                    {
                        continue;
                    }
                    if (jk == l || jk == l || all_ant[l].name_kol == all_ant[jk].name_kol)
                    {
                        break;
                    }

                    if (all_ant[l].Proffessy == Proffessy.ARMY)
                    {
                        all_ant[l].health = 100;
                    }

                    if (all_ant[jk].Proffessy == Proffessy.ARMY)
                    {
                        all_ant[jk].health = 100;
                    }

                    while (all_ant[l].health > 0 && all_ant[jk].health > 0)
                    {
                        while (all_ant[l].defence > 0 && all_ant[jk].defence > 0)
                        {
                            all_ant[jk].defence -= all_ant[l].damage;
                            all_ant[l].defence -= all_ant[jk].damage;
                        }
                        all_ant[jk].health -= all_ant[l].damage;


                        if (all_ant[jk].health <= 0)
                        {
                            break;
                        }

                        all_ant[l].health -= all_ant[jk].damage;
                        
                        if (all_ant[l].health <= 0)
                        {
                            break;
                        }
                        // Console.WriteLine($"health1: {all_ant[jk].health} health2: {all_ant[l].health}");
                    }
                }

                for (int j = 0; j < all_ant.Count; j++)
                {
                    if (all_ant[j].health == 0)
                    {
                        all_ant.RemoveAt(j);
                    }
                }

                for (int j = 0; j < all_ant.Count; j++)
                {
                    if (all_ant[j].health == 0)
                    {
                        all_ant.RemoveAt(j);
                    }
                }

                for (int j = 0; j < all_ant.Count; j++)
                {
                    if (all_ant[j].health == 0)
                    {
                        all_ant.RemoveAt(j);
                    }
                }

                for (int j = 0; j < all_ant.Count; j++)
                {
                    if (all_ant[j].health == 0)
                    {
                        all_ant.RemoveAt(j);
                    }
                }

                // Console.WriteLine("------------------------------");
                // foreach (var bh in all_ant)
                // {
                //     Console.WriteLine($"kolony: {bh.name_kol}, health: {bh.health}, proffessy {bh.Proffessy}");
                // }
                // Console.WriteLine("------------------------------");

                
                for (int j = 0; j < mass_kolonys.Count; j++)
                {
                    int old_rab = kolonias[mass_kolonys[j]].popul_work();
                    int old_voin = kolonias[mass_kolonys[j]].popul_war();
                    int old_spec = kolonias[mass_kolonys[j]].popul_spec();
                    List<Ant> kol_ants = new List<Ant>() { };
                    kolonias[mass_kolonys[j]].Ants = kol_ants;

                    for (int k = 0; k < all_ant.Count; k++)
                    {
                        if (kolonias[mass_kolonys[j]].name_kol == all_ant[k].name_kol)
                        {
                            kolonias[mass_kolonys[j]].Ants.Add(all_ant[k]);
                        }
                    }
                    
                    Console.WriteLine($"В Kолонию <<{kolonias[mass_kolonys[j]].name_kol}>> {mass_kolonys[j]} вернулись:");
                    Console.WriteLine($"---рабочие: {kolonias[mass_kolonys[j]].popul_work()} военные: {kolonias[mass_kolonys[j]].popul_war()} особые: {kolonias[mass_kolonys[j]].popul_spec()}");
                    Console.WriteLine($"---потери:  р: {old_rab - kolonias[mass_kolonys[j]].popul_work()} в: {old_voin - kolonias[mass_kolonys[j]].popul_war()} о: {old_spec - kolonias[mass_kolonys[j]].popul_spec()}");
                }
                

                // Console.WriteLine("1");
            }
        }

        raspred_po_koloniam(ref kuchi_kolonii,ref kuchas,ref kolonias);
    }

    public static void raspred_po_koloniam(ref Dictionary<int, List<int>> kuchi_kolonii,ref List<Kucha> kuchas,ref List<Kolonia> kolonias)
    {
        // Console.WriteLine("2.2");
        int size_kolony = kuchi_kolonii.ElementAt(0).Value.Count;
        int num = 0;
        // Console.WriteLine(kolonias.Count);
        // for (int i = 0; i < kolonias.Count; i++)
        // {
        //     Console.WriteLine($"Kolony: {i} k: {kolonias[i].k} l: {kolonias[i].l} v: {kolonias[i].v} r: {kolonias[i].r}");
        // }
        //
        // foreach (var kl in kuchas)
        // {
        //     Console.WriteLine($"Kucha {kl} k: {kl.k} l: {kl.l} v: {kl.v} r: {kl.r}");
        // }
        
        List<int> active_ant_list = new List<int>() { };
        List<List<int>> mass_need_resourses = new List<List<int>>() { };
        Console.WriteLine("Добыто ресурсов: ");
        for (int i = 0; i < kuchi_kolonii.Count; i++)
        {
            var item = kuchi_kolonii.ElementAt(i);
            var kucha = item.Key;
            var kolony = item.Value;
            
            if (size_kolony == 1)
            {
                int active_ants = 0;
                
                int old_k = kolonias[kolony[0]].k;
                int old_l = kolonias[kolony[0]].l;
                int old_v = kolonias[kolony[0]].v;
                int old_r = kolonias[kolony[0]].r;
            
                for (int j = 0; j < kolonias[kolony[0]].Ants.Count; j++)
                {
                    if (kolonias[kolony[0]].Ants[j].Proffessy == Proffessy.WORKER || kolonias[kolony[0]].Ants[j].Proffessy == Proffessy.SPECIAL)
                    {
                        active_ants += 1;
                    }
                }

                while (active_ants > 0)
                {
                    // Console.WriteLine("2.2.0");
                    // Console.WriteLine(active_ants);
                    if (kuchas[kucha].k >= 1)
                    {
                        kolonias[kolony[0]].k += 1;
                        active_ants -= 1;
                        kuchas[kucha].k -= 1;
                        if (active_ants == 0)
                        {
                            break;
                        }
                    }
                    
                    if (kuchas[kucha].l >= 1)
                    {
                        kolonias[kolony[0]].l += 1;
                        active_ants -= 1;
                        kuchas[kucha].l -= 1;
                        if (active_ants == 0)
                        {
                            break;
                        }
                    }
                    
                    if (kuchas[kucha].v >= 1)
                    {
                        kolonias[kolony[0]].v += 1;
                        active_ants -= 1;
                        kuchas[kucha].v -= 1;
                        if (active_ants == 0)
                        {
                            break;
                        }
                    }
                    
                    if (kuchas[kucha].r >= 1)
                    {
                        kolonias[kolony[0]].r += 1;
                        active_ants -= 1;
                        kuchas[kucha].r -= 1;
                        if (active_ants == 0)
                        {
                            break;
                        }
                    }

                    if (kuchas[kucha].r == 0 && kuchas[kucha].l == 0 && kuchas[kucha].v == 0 && kuchas[kucha].k == 0)
                    {
                        break;
                    }
                }

                Console.WriteLine($"---добыто ресурсов колония <<{kolonias[kolony[0]].name_kol}>> {kolony[0]}: k={kolonias[kolony[0]].k - old_k} l={kolonias[kolony[0]].l - old_l} v={kolonias[kolony[0]].v - old_v} r={kolonias[kolony[0]].r - old_r}");
                Console.WriteLine();
                // Console.WriteLine("2.2.1");
            }
            

            if (size_kolony > 1)
            {
                for (int j = 0; j < kolony.Count; j++)
                {
                    int active_ant = 0;
                    for (int k = 0; k < kolonias[kolony[j]].Ants.Count; k++)
                    {
                        if (kolonias[kolony[j]].Ants[k].Proffessy == Proffessy.WORKER ||
                            kolonias[kolony[j]].Ants[k].Proffessy == Proffessy.SPECIAL)
                        {
                            active_ant += 1;
                        }
                    }

                    int old_k = kolonias[kolony[j]].k;
                    int old_l = kolonias[kolony[j]].l;
                    int old_v = kolonias[kolony[j]].v;
                    int old_r = kolonias[kolony[j]].r;
                    active_ant_list.Add(active_ant);

                    for (int l = 0; l < active_ant_list.Count; l++)
                    {
                        while (active_ant_list[l] > 0)
                        {
                            if (kuchas[kucha].k >= 1)
                            {
                                kolonias[kolony[j]].k += 1;
                                active_ant_list[l] -= 1;
                                kuchas[kucha].k -= 1;
                                if (active_ant_list[l] == 0)
                                {
                                    break;
                                }
                            }

                            if (kuchas[kucha].l >= 1)
                            {
                                kolonias[kolony[j]].l += 1;
                                active_ant_list[l] -= 1;
                                kuchas[kucha].l -= 1;
                                if (active_ant_list[l] == 0)
                                {
                                    break;
                                }
                            }

                            if (kuchas[kucha].v >= 1)
                            {
                                kolonias[kolony[j]].v += 1;
                                active_ant_list[l] -= 1;
                                kuchas[kucha].v -= 1;
                                if (active_ant_list[l] == 0)
                                {
                                    break;
                                }
                            }

                            if (kuchas[kucha].r >= 1)
                            {
                                kolonias[kolony[j]].r += 1;
                                active_ant_list[l] -= 1;
                                kuchas[kucha].r -= 1;
                                if (active_ant_list[l] == 0)
                                {
                                    break;
                                }
                            }
                            
                            if (kuchas[kucha].r == 0 && kuchas[kucha].l == 0 && kuchas[kucha].v == 0 && kuchas[kucha].k == 0)
                            {
                                break;
                            }
                        }

                        // Console.WriteLine("2.3");
                    }
                    Console.WriteLine($"---добыто ресурсов колония <<{kolonias[kolony[j]].name_kol}>> {kolony[j]}: k={kolonias[kolony[j]].k - old_k} l={kolonias[kolony[j]].l - old_l} v={kolonias[kolony[j]].v - old_v} r={kolonias[kolony[j]].r - old_r}");
                    Console.WriteLine();
                    // Console.WriteLine("2.4");
                }

                // Console.WriteLine("2.5");
            }

            // Console.WriteLine("2");
        }

        // Console.WriteLine("--------------------------");
        // Console.WriteLine("---------------------------------");
        // foreach (var kl in kuchas)
        // {
        //     Console.WriteLine($"Kucha {kl} k: {kl.k} l: {kl.l} v: {kl.v} r: {kl.r}");
        // }
        //
        // Console.WriteLine(kolonias.Count);
        // for (int i = 0; i < kolonias.Count; i++)
        // {
        //     Console.WriteLine($"Kolony: {i} k: {kolonias[i].k} l: {kolonias[i].l} v: {kolonias[i].v} r: {kolonias[i].r}");
        // }
        
    }

}

public class Kolonia
    {
        public string name_kol { get; set; }

        public King king { get; set; }

        // public List<WarAnt> WarAnts { get; set; }
        // public List<WorkAnt> WorkAnts { get; set; }
        // public List<SpecialInc> specialIncs { get; set; }
        public int k { get; set; }
        public int l { get; set; }
        public int v { get; set; }
        public int r { get; set; }
        public List<Ant> Ants { get; set; }
        public List<King> new_Kings { get; set; }
        public int max_num { get; } = 24;

        public Kolonia(string name_kol, List<Ant> Ants, King king, int k, int l, int v, int r, List<King> new_Kings)
        {
            this.name_kol = name_kol;
            this.king = king;
            // this.WarAnts = WarAnts;
            // this.WorkAnts = WorkAnts;
            // this.specialIncs = specialIncs;
            this.k = k;
            this.l = l;
            this.v = v;
            this.r = r;
            this.Ants = Ants;
            this.new_Kings = new_Kings;
        }

        public int populate()
        {
            return Ants.Count + 1;
        }

        public int resourses()
        {
            return k + l + r + v;
        }
        
        public int popul_work()
        {
            int count = 0;
            for (int i = 0; i < Ants.Count; i++)
            {
                if (Ants[i].Proffessy == Proffessy.WORKER)
                {
                    count += 1;
                }
            }

            return count;
        }
        
        public int popul_war()
        {
            int count = 0;
            for (int i = 0; i < Ants.Count; i++)
            {
                if (Ants[i].Proffessy == Proffessy.ARMY)
                {
                    count += 1;
                }
            }

            return count;
        }
        
        public int popul_spec()
        {
            int count = 0;
            for (int i = 0; i < Ants.Count; i++)
            {
                if (Ants[i].Proffessy == Proffessy.SPECIAL)
                {
                    count += 1;
                }
            }

            return count;
        }
        
        public int popul_full()
        {
            return popul_spec() + popul_war() + popul_work();
        }
    }

    public class King
    {
        public string name_k { get; }
        public string name_mom_k { get; set; }
        public int kids_ant { get; set; }
        public int kids_kings { get; set; }
        
        public List<Ant> lichinki_ant { get; set; }
        public List<King> lichinki_king { get; set; }
        public string name_kol { get; }

        public King(string name_k, int kids_ant, int kids_kings, string name_kol, string name_mom_k)
        {
            this.kids_ant = kids_ant;
            this.kids_kings = kids_kings;
            this.name_k = name_k;
            this.name_kol = name_kol;
            this.name_mom_k = name_mom_k;
        }
    }

// public class WorkAnt : Ant
// {
//     public WorkAnt(string name_a, string name_k, int kids, King king, Proffessy Proffessy) : base(name_a, name_k, kids, king, Proffessy)
//     {
//         
//     }
//     
//     public override void TalkAboutYou()
//     {
//         Console.WriteLine($"Меня зовут: {name_a}");
//         Console.WriteLine($"Моя королева: {king.name_k}");
//         Console.WriteLine("Я рабочий");
//     }
// }

// public class WarAnt : Ant
// {
//     public override int health { get; set; } = 100;
//     public WarAnt(string name_a, string name_k, int kids, King king, Proffessy Professy) : base(name_a, name_k, kids, king, Professy)
//     {
//     }
//     
//     public override void TalkAboutYou()
//     {
//         Console.WriteLine($"Меня зовут: {name_a}");
//         Console.WriteLine($"Моя королева: {king.name_k}");
//         Console.WriteLine("Я военный");
//     }
// }

// public class SpecialInc : Ant
// {
//     public SpecialInc(string name_a, string name_k, int kids, King king, Proffessy Professy) : base(name_a, name_k, kids, king, Professy)
//     {
//     }
//     
//     public override void TalkAboutYou()
//     {
//         Console.WriteLine($"Меня зовут: {name_a}");
//         Console.WriteLine($"Моя королева: {king.name_k}");
//         Console.WriteLine("Я спецнасекомое");
//     }
// }

    public enum Proffessy
    {
        WORKER,
        ARMY,
        KING,
        SPECIAL
    }

    public class Ant : King
    {
        public King king { get; set; }
        public string type { get; }

        public virtual int health { get; set; } = 10;
        public int damage { get; } = 10;
        public int defence { get; set; } = 10;
        public Proffessy Proffessy { get;}
        

        public Ant(string type, string name_k, King king, Proffessy Proffessy, string name_kol, int kids_ant, int kids_kings, string name_mom_k) : base(name_k, kids_ant, kids_kings, name_kol, name_mom_k)
        {
            this.type = type;
            this.king = king;
            this.Proffessy = Proffessy;
        }



        // public string TalkAboutYour()
        // {
        //     
        //     return $"Меня зовут: {name_a}";
        // }
        //
        // public string TalkAboutKing(King king)
        // {
        //     return $"Моя королева: {king.name_k}";
        // }

        public virtual void TalkAboutYou()
        {
            Console.WriteLine($"Имя: {type}");
            Console.WriteLine($"Моя королева: {king.name_k}");
            Console.WriteLine($"Моя профессия {Proffessy}");
            Console.WriteLine($"Мое здоровье {health}");
            Console.WriteLine($"Моя защита {defence}");
        }

        public int Damage()
        {
            return health - 10;
        }
    }

    public class Kucha
    {
        public int k { get; set; }
        public int l { get; set; }
        public int v { get; set; }
        public int r { get; set; }

        public Kucha(int k, int l, int v, int r)
        {
            this.k = k;
            this.l = l;
            this.v = v;
            this.r = r;
        }
    }
    