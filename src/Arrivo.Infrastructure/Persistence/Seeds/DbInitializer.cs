using Arrivo.Domain.Entities;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Arrivo.Infrastructure.Persistence.Seeds;

public static class DbInitializer
{
    private sealed record RItem(string Name, string Price);
    private sealed record RCat(string Name, RItem[] Items);
    private sealed record RDef(string Name, string Phone, string Saat, RCat[] Cats);

    private static readonly RDef[] AllRestaurants =
    [
        new("Adıyaman Çiğ Köftecisi", "+90-534-514-4113", "16:00-05:00",
        [
            new("TOST", [new("Karışık YARIM EKMEK Tost", "200₺"), new("kaşarlı tost", "175₺")]),
            new("parmak patates kızartması", [new("parmak patates kızartması", "250₺"), new("Doritos", "80₺")]),
            new("Tatlı", [new("Keşkül", "70₺"), new("Puding", "70₺"), new("Muhallebi", "70₺"), new("Sütlaç", "70₺"), new("Profiterol", "70₺"), new("Kazandibi", "70₺"), new("Sakızlı Muhallebi", "70₺"), new("Supangle", "70₺"), new("Tavukgöğsü", "70₺")]),
            new("Porsiyonlar", [new("Yarım Porsiyon", "300₺"), new("Tam Porsiyon", "450₺"), new("Bir Buçuk Porsiyon", "550₺"), new("Bir Kilo Porsiyon", "650₺"), new("Garnitürsüz Çiğköfte Porsiyon", "600₺")]),
            new("Dürümler", [new("Öğrenci Dürüm", "110₺"), new("Normal Dürüm", "130₺"), new("Dolgun Dürüm", "140₺"), new("Mega Dürüm", "150₺"), new("Jumbo Dürüm", "160₺"), new("içli köfte adet", "100₺")]),
            new("İçecekler", [new("Coca Cola", "70₺"), new("Coca Cola Zero", "70₺"), new("Sprite", "70₺"), new("Fanta", "70₺"), new("Cappy (Vişne, Şeftali)", "70₺"), new("Fuse Tea (Şeftali, Limon, Mango Ananas Çilek, Frambuaz, Böğürtlen)", "70₺"), new("Soda (Limonlu, Elmalı)", "60₺"), new("Soda (Sade)", "50₺"), new("Büyük Ayran", "50₺"), new("Küçük Ayran", "40₺"), new("Şalgam", "50₺"), new("1 Litre Cola-Fanta-Sprite", "100₺"), new("Su", "20₺"), new("2.5 Litre cola", "150₺"), new("1 Litre Ayran", "100₺"), new("ADANA şalgam 1 lt", "100₺"), new("Fusetea  KAVUN VE CİLEKLİ 1 LT", "100₺"), new("ADANA ŞİŞE ŞALGAM", "40₺"), new("fısetea EJDER VE CARKIFELEK MEYVE AROMALI 1LT", "100₺"), new("fusetea MANGO VE ANANAS AROMALI 1LT", "100₺"), new("fusetea ŞEFTALİ AROMALI 1LT", "100₺"), new("fusetea KARPUZ AROMALI 1LT", "100₺"), new("su 1.5 litre", "50₺"), new("Schweppes TONIC WATER", "100₺"), new("Fusetea  LİMON AROMALI 1 LT", "100₺"), new("su 5 Litre", "80₺"), new("Monster Enerji", "100₺"), new("Powerade", "100₺"), new("Burn Enerji", "100₺"), new("Eker Kefir Litrelik (Orman Meyvesi, Çilekli)", "150₺"), new("Eker Kefir Küçük (Orman Meyveli, Çilekli, Şeftali - Kayısılı)", "60₺")])
        ]),
        new("Adwire Bohemian Fast Food", "+90-561-610-5487", "12:00-04:00",
        [
            new("Chicken Fried", [new("Chicken Burger", "200₺"), new("Chicken Royale", "250₺"), new("Çıtır Burger", "300₺"), new("Cheese & Chick Bun", "275₺"), new("Çıtır Bun", "325₺"), new("Tenders 2'li", "100₺"), new("Nugget 4'lü", "100₺")]),
            new("Vegan & Vejeteryan", [new("Bun Burger (Vegan)", "200₺"), new("Falafel Burger (Vejeteryan)", "-"), new("Kremalı Mantarlı Penne", "250₺"), new("Rice Stick", "350₺"), new("Sebzeli Nuddle", "300₺")]),
            new("Mantılar", [new("Fırın Mantı", "150₺"), new("Bohça Mantı", "200₺"), new("Kayseri Mantısı", "250₺"), new("Tortellini", "300₺")]),
            new("Ekmek Arası", [new("Patso", "145₺"), new("Kremalı Tavuk", "175₺"), new("Tavuk Sote", "175₺"), new("Köfte", "185₺"), new("Döner", "195₺"), new("Karışık", "200₺")]),
            new("Atıştırmalıklar", [new("Patates Kızartması", "175₺"), new("Soğan Halkası 6'lı", "125₺"), new("Nugget 6'lı", "150₺"), new("Mozarella Sticks 4'lü", "175₺"), new("Çıtır Sepeti", "300₺"), new("Mega Çıtır Sepeti", "400₺"), new("Mozeralla ilavesi", "20₺"), new("Cheddar sos ilavesi", "20₺"), new("Cheddar ilavesi", "30₺"), new("Vegan Çıtır Sepeti", "350₺"), new("Patates Kroket 6'lı", "150₺"), new("Vegan İçli Köfte 4'lü", "200₺")]),
            new("Dürümler", [new("Tavuk Sote", "175₺"), new("Kremalı Tavuk", "175₺"), new("Döner", "185₺"), new("Adana", "195₺"), new("Urfa", "195₺"), new("patso", "145₺"), new("kaşar ilavesi", "25₺"), new("patates ilavesi", "40₺"), new("mozarella ilavesi", "25₺"), new("cheddar sos ilavesi", "25₺"), new("cheddar peynir ilavesi", "40₺")]),
            new("Tombik", [new("Patso", "185₺"), new("Döner", "260₺"), new("Köfte", "240₺"), new("Karışık", "260₺"), new("Sokak Smash", "345₺"), new("kremalı tavuk", "240₺"), new("tavuk sote", "240₺")]),
            new("Sandviçler", [new("Kaşarlı Ayvalık Tostu", "145₺"), new("Sucuklu Ayvalık Tostu", "180₺"), new("Karışık Ayvalık Tostu", "210₺"), new("Special Ayvalık Tostu", "260₺"), new("Harby's", "290₺")]),
            new("Hamburger", [new("Hamburger", "290₺"), new("Rodeo Burger", "350₺"), new("Smash Burger", "365₺"), new("Adwire Bohemian Special Burger", "450₺"), new("Cheeseburger", "320₺"), new("Yellow Burger", "365₺"), new("Smoke Burger", "365₺"), new("Onion Burger", "365₺"), new("Red Burger", "365₺"), new("Kore Burger", "-"), new("Duble Kore Sos", "-")]),
            new("Pilav", [new("Tavuklu Pilav", "150₺"), new("Tavuk Sote Pilav", "150₺"), new("Köfte Pilav", "195₺"), new("Pilav Üstü Et Döner", "215₺"), new("sade pilav", "100₺"), new("kuşbaşı tavuk pilav", "150₺")]),
            new("Makarna", [new("Napoliten", "175₺"), new("Bolonez", "250₺"), new("Alfredo", "250₺"), new("Curry", "250₺"), new("Mac & Cheese tavuklu", "300₺"), new("Tavuklum", "300₺"), new("Arabiata", "175₺"), new("mac & cheese", "250₺"), new("Tagliatelle (Hot and Sour)", "275₺"), new("Tagliattelle Schnitzel (Pesto)", "350₺"), new("Tagliattelle Tenders (Hot and  Sour)", "425₺")]),
            new("Special", [new("Tavuk Kanat", "180₺"), new("Köfte Porsiyon", "300₺"), new("İskender", "300₺"), new("Adana Kebap", "375₺"), new("Urfa Kebap", "375₺"), new("Beyti Sarma", "375₺"), new("Bohemian Kebap", "500₺"), new("Kore Usulü Tavuk Kanat", "220₺"), new("kore usulü tavuk kuşbaşı", "250₺"), new("tavuk kapama", "275₺")]),
            new("Sıcak İçecekler", [new("Çay", "25₺"), new("Türk Kahvesi", "50₺"), new("Türk Kahvesi Double", "75₺"), new("Filtre Kahve", "75₺"), new("Nescafe", "50₺"), new("Sıcak Çikolata", "75₺"), new("Sahlep", "75₺")]),
            new("Soğuk İçecekler", [new("Erikli Su 500 ml", "20₺"), new("Soda", "25₺"), new("Meyveli Soda", "30₺"), new("Nescafe Express", "60₺"), new("Cola Turka kutu 330 ml", "50₺"), new("Coca Cola / Coca Cola Zero kutu 330 ml", "70₺"), new("Küçük Ayran 175 ml", "30₺"), new("Büyük Ayran 300 ml", "50₺"), new("Redbull 250 ml", "75₺"), new("Litrelik Cola Turka", "70₺"), new("Litrelik Coca Cola / Coca Cola Zero", "90₺"), new("Bardak İçecek 12 oz", "30₺"), new("Fuse Tea 330 ml", "70₺"), new("Lipton Icetea 330 ml", "70₺"), new("pepsi kutu 330 ml", "60₺"), new("pepsi 1 litre", "80₺"), new("coca cola 2.5 litre", "110₺"), new("coca cola kutu 250 ml", "55 ₺")])
        ]),
        new("Arya Kasap Et Pişiricisi", "+90-538-934-2007", "11:00-02:00",
        [
            new("Dürüm & Ekmek Arası", [new("Tavuk İncik + Ayran", "150₺"), new("Köfte + Ayran", "150₺"), new("Ciğer + Ayran", "150₺"), new("Adana+ Ayran", "150₺"), new("Sucuk+ Ayran", "150₺")]),
            new("Porsiyon", [new("Tavuk İncik", "180₺"), new("Tavuk Göğüs", "180₺"), new("Tavuk Kanat", "180₺"), new("Sucuk", "300₺"), new("Köfte", "300₺")]),
            new("İçecekler", [new("Kola", "60₺"), new("Fanta", "60₺"), new("Sprite", "60₺"), new("Şalgam", "60₺"), new("Ayran", "25₺"), new("Tombul Ayran", "50₺"), new("Su", "20₺"), new("Litrelik Kola", "80₺"), new("Litrelik Ayran", "80₺")])
        ]),
        new("Arya Pide", "+90-544-740-1571", "12:00-02:00",
        [
            new("Pideler ve Lahmacun", [new("Kıymalı Sade", "200₺"), new("Kıymalı Yumurtalı", "225₺"), new("Kıymalı Kaşarlı", "250₺"), new("Kuşbaşılı Sade", "325₺"), new("Kuşbaşılı Kaşarlı", "350₺"), new("Lahmacun", "200₺"), new("Lahmacun Kaşarlı", "225₺"), new("Arya Spesiyal", "300₺")]),
            new("Çorbalar", [new("Ezo Gelin", "180₺"), new("Mercimek", "180₺"), new("İşkembe", "230₺"), new("Kelle Paça", "230₺")]),
            new("Izgaralar ve Kebaplar", [new("Full Karışık", "500₺"), new("Adana", "350₺"), new("Urfa", "350₺"), new("Beyti Sarma", "450₺"), new("Tavuk Şiş", "350₺"), new("Kanat Izgara", "400₺")])
        ]),
        new("Atölye Plus(+++)", "+90-507-503-8083", "12:00-02:30",
        [
            new("DOYURAN MENÜ", [new("Doyuran Dürüm+Eker Tatlı+Eker Büyük Ayran", "250₺")]),
            new("CRISPY CHICKEN WRAP", [new("Atölye Özel Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Sriracha Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Acılı Mayonez Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Ranch Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Sezar Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Tatlı-Ekşi Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Tatlı-Acı Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Ballı Hardal Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Barbeqü Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Acı Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Chedar Soslu Crispy Chicken Wrap+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺")]),
            new("CRISPY CHICKEN BURGER", [new("Crispy Chicken Burger+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺")]),
            new("ÇEK Bİ EKMEK ARASI", [new("Tavuk İncik+Patates+Eker Tatlı+Eker Büyük Ayran", "250₺"), new("Kasap Köfte+Patates+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Kangal Sucuk+Patates+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Kavurma+Patates+Eker Tatlı+Eker Büyük Ayran", "395₺")]),
            new("ÇILGIN MENÜLER", [new("Hamburger(Et 120gr)+Patates+Kutu İçecek+Eker Tatlı", "500₺"), new("Cheese Burger(Et 120gr)+Patates+Kutu İçecek+Eker Tatlı", "520₺"), new("Atölye Karışık Pizza+Patates+Kutu İçecek+Eker Tatlı", "500₺"), new("Margarita Pizza+Patates+Kutu İçecek+Eker Tatlı", "445₺"), new("Alfredo Penne+Kutu İçecek+Eker Tatlı", "345₺"), new("Pesto Penne+Kutu İçecek+Eker Tatlı", "345₺"), new("Köri Penne+Kutu İçecek+Eker Tatlı", "345₺")]),
            new("VEGAN MENÜ", [new("Vegan Snitzel Wrap(150 gr) +Patates", "330₺"), new("Vegan Döner Wrap(120 gr) +Patates", "330₺"), new("Vegan Köfte Wrap(180 gr) +Patates", "370₺"), new("Vegan Snitzel Burger(220 gr) +Patates", "340₺"), new("Vegan Köfte Burger(220 gr) +Patates", "430₺"), new("Vegan Döner Burger(150 gr) +Patates", "340₺"), new("Vegan Snitzel Salata(220 gr)", "370₺"), new("Vegan Döner Salata(200 gr)", "370₺"), new("Vegan Köfte Salata (250 gr)", "400₺")]),
            new("ATIŞTIRMALIKLAR", [new("ATOLYE SPECIAL KOVA (1000gr Tavuk) 3-4 Kişilik+Patates+5'li Colorado Sos Seti", "650₺"), new("ATOLYE SPECIAL KANAT (1000gr kanat) 3-4 Kişilik + Patates + 5lİ Küvet Sos", "750₺"), new("Sıcak Sepeti", "315₺"), new("ATOLYE KOVA", "450₺"), new("ATOLYE LUX KOVA 3-4 Kişilik", "530₺"), new("12'li Soğan Halkası + Patates", "315₺"), new("12'li Sosis Sepeti + Patates", "315₺"), new("12'li Nugget Sepeti + Patates", "315₺"), new("PATATES KIZARTMASI", "200₺")]),
            new("ATÖLYE PİZZA", [new("Margarita Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "395₺"), new("Mushroom Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "395₺"), new("Vegetable Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("Atolye Karışık Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("Füme Kaburga Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "490₺"), new("Pizza Pepperoni + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("Polo Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("4 Peynirli Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "395₺"), new("Mexican Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("Ton Balıklı Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "495₺"), new("Kavurmalı Pizza + Patates+Eker Tatlı+Eker Büyük Ayran", "520₺")]),
            new("BURGERLER", [new("Hamburger (Et 120gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "450₺"), new("Cheese Burger (Et 120gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "470₺"), new("Double Burger (Et 240gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "650₺"), new("Dana Kaburga Füme Hamburger (Et 120gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "550₺"), new("Chicken Burger (Tavuk 120 gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("BBQ Mushroom Burger (Et 120gr) + Patates+Eker Tatlı+Eker Büyük Ayran", "470₺"), new("Double Chicken Burger (Tavuk 240gr)+Patates+Eker Tatlı+Eker Büyük Ayran", "395₺"), new("Sucuk Burger+Patates+Eker Tatlı+Eker Büyük Ayran", "325₺")]),
            new("MAKARNALAR", [new("Alfredo Penne+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Köri Penne+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Pesto Penne+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Mantarlı, Biberli Tavuklu Penne+Eker Tatlı+Eker Büyük Ayran", "295₺"), new("Ton Balıklı Penne+Eker Tatlı+Eker Büyük Ayran", "350₺"), new("Spagetti Arabiata+Eker Tatlı+Eker Büyük Ayran", "290₺"), new("Spagetti Napolitan+Eker Tatlı+Eker Büyük Ayran", "250₺")]),
            new("TOSTLAR", [new("Kavurmalı Kaşarlı Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "310₺"), new("Karışık Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "260₺"), new("Kaşarlı, Cheddar Peynirli Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "240₺"), new("Mantarlı, Kaşarlı Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "250₺"), new("Mantarlı, Kaşarlı, Kavurmalı Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "325₺"), new("Kaşarlı Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "230₺"), new("Sucuklu Kaşarlı Tost + Patates+Eker Tatlı+Eker Büyük Ayran", "250₺")]),
            new("BAZLAMA TOSTLAR", [new("Kaşarlı Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "250₺"), new("Karışık Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "300₺"), new("Mantarlı Kaşarlı Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "280₺"), new("Üç Peynirli Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "290₺"), new("Kavurmalı Kaşarlı Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "350₺"), new("Special Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "390₺"), new("Nutella Bazlama", "250₺"), new("Muzlu Nutella Bazlama", "270₺"), new("Sucuklu Kaşarlı Bazlama+Patates+Eker Tatlı+Eker Büyük Ayran", "300₺")]),
            new("AYVALIK TOSTLARI", [new("Kavurmalı Kaşarlı Ayvalık Tostu + Patates+Eker Tatlı+Eker Büyük Ayran", "350₺"), new("Süper Karışık Ayvalık Tostu + Patates+Eker Tatlı+Eker Büyük Ayran", "300₺"), new("Karışık Ayvalık + Patates+Eker Tatlı+Eker Büyük Ayran", "290₺")]),
            new("DÜRÜMLER", [new("Tavuk Döner Dürüm+Patates+Eker Tatlı+Eker Büyük Ayran", "290₺"), new("Kavurmalı Dürüm (100gr Et)+Patates+Eker Tatlı+Eker Büyük Ayran", "350₺")]),
            new("WRAPLER", [new("Tavuk Wrap+Patates + Eker Tatlı + Eker Büyük Ayran", "325₺"), new("BBQ Soslu Tavuk Wrap + Patates+Eker Tatlı + Eker Büyük Ayran", "325₺"), new("Ballı Hardal Soslu Tavuk Wrap + Patates+Eker Tatlı +Eker Büyük Ayran", "325₺")]),
            new("KUMPİR", [new("Atölye Kumpir", "350₺"), new("Tavuklu Kumpir", "370₺"), new("Dana Füme Kaburga Kumpir", "390₺"), new("Ton Balıklı Kumpir", "390₺")]),
            new("SALATALAR", [new("Tavuklu Sezar Salata", "295₺"), new("Çıtır Tavuk Salata", "295₺"), new("Ton Balıklı Salata", "350₺")]),
            new("NESTLE WAFFLE", [new("Çilekli Nestle Waffle", "295₺"), new("Çilekli-Muzlu Nestle Waffle", "295₺"), new("Muzlu Nestle Waffle", "295₺"), new("Çilekli Muzlu Antep Fıstık Soslu", "295₺"), new("Çilekli Muzlu Bitter Çikolata Soslu", "295₺"), new("Çilekli Muzlu Dubai Çikolata Soslu", "295₺"), new("Çilekli Muzlu Beyaz Çikolata Soslu", "295₺")]),
            new("KÜVET SOSLAR", [new("COLORADO RANCH SOS 20 GR", "10₺"), new("COLORADO HOT CHILI SOS 20 GR", "10₺"), new("COLORADO ET SOS BARBEQUE 22 GR", "10₺"), new("COLORADO ET SOS GARLIC 20 GR", "10₺"), new("COLORADO HONEY MUSTARD SOS 20 GR", "10₺")]),
            new("VİTAMİN BAR", [new("Taze Sıkım Portakal Suyu", "130₺"), new("Taze Sıkım Limon, Portakal Suyu", "140₺")]),
            new("İÇECEKLER", [new("Coca-Cola Zero", "85₺"), new("Coca-Cola", "85₺"), new("Fanta", "85₺"), new("Sprite", "85₺"), new("Fuse Tea Karpuz", "85₺"), new("Niğde Gazozu", "80₺"), new("Eker Büyük Ayran", "60₺"), new("Soda", "60₺"), new("Limonlu Soda", "70₺"), new("Şişe Su", "30₺"), new("Red Bull", "120₺"), new("Karpuz-Çilek Soda", "70₺"), new("Churchill", "85₺"), new("Fuse Tea Şeftali", "85₺"), new("Fuse Tea Limon", "85₺"), new("Fuse Tea Mango ve Ananas", "85₺"), new("Karadut ve Frenk Üzümlü Soda", "70₺"), new("Fuse Tea Kavun Çilek", "85₺"), new("Elmalı Soda", "70₺"), new("Zafer Gazozu", "80₺"), new("Süt 1L", "150₺")])
        ]),
        new("Bani Urla", "+90-530-404-2644", "13:00-02:30",
        [
            new("Wrapler", [new("Tavuk Wrap", "255₺"), new("BBQ Tavuk Wrap", "275₺"), new("Bani Çıtır Wrap", "255₺")]),
            new("Burgerler", [new("Chicken Burger", "255₺"), new("Double Chicken Burger", "355₺")]),
            new("Makarnalar", [new("Alfredo Penne", "255₺"), new("Köri Penne", "255₺"), new("Pesto Penne", "255₺")]),
            new("Atıştırmalıklar", [new("Bira tabağı", "315₺"), new("Tuzlu Fıstık", "110₺"), new("Patates Kızartması", "195₺"), new("Turşu", "60₺"), new("Crispy Chicken", "255₺"), new("Doritos Taco Cips", "120₺")]),
            new("Soğuk İçecekler", [new("Pepsi", "70₺"), new("Pepsi Zero Sugar", "70₺"), new("Sprite", "70₺"), new("Red Bull", "120₺"), new("Red Bull Sugar Free", "120₺"), new("Fuse Tea Şeftali", "70₺"), new("Fuse Tea Limon", "70₺"), new("Fuse Tea Mango&Ananas", "70₺"), new("Uludağ Su", "25₺"), new("Maden Suyu", "70₺"), new("Churchill", "120₺"), new("Schweppes Cam Tonic", "100₺"), new("Ayran", "30₺"), new("Büyük Ayran", "60₺"), new("Yedigün", "70₺")]),
            new("Sıcak İçecekler", [new("Fincan Çay", "60₺"), new("Türk Kahvesi", "115₺"), new("Duble Türk Kahvesi", "150₺")])
        ]),
        new("Bastards Burger", "+90-532-423-9393", "12:00-24:00",
        [
            new("BURGERLER", [new("DIRTY BASTARD", "150₺"), new("CLASSIC BASTARD", "350₺"), new("CHEESY BASTARD", "370₺"), new("LUCKY BASTARD", "495₺"), new("CHEF'S BASTARD", "550₺")]),
            new("TAVUK", [new("CRAZY CHICKEN KUTU", "275₺"), new("SUPER CRAZY CHICKEN KOVA", "550₺"), new("CHICKEN BASTARD", "315₺")]),
            new("ÖZEL SOSLAR", [new("BALKABAKLI", "30₺"), new("SRIRACHA MAYONEZ", "30₺"), new("ALLIOLI", "30₺"), new("LIME YOĞURT SOS", "30₺")]),
            new("PATATES KIZARTMASI", [new("PARMAK PATATES 150g", "120₺")]),
            new("İÇECEK", [new("COCA-COLA/FANTA/SPRITE/FUSE TEA (200ml)", "55₺"), new("COCA-COLA/FANTA/SPRITE/FUSE TEA (330ml)", "85₺"), new("AYRAN", "45₺"), new("SU", "30₺"), new("SODA", "40₺")])
        ]),
        new("Bükre Cafe", "+90-505-973-8485", "09:00-02:00",
        [
            new("Menü", [new("Kahvaltı Tabağı", "200₺"), new("Kaşarlı Menemen", "130₺"), new("Kayseri Mantı", "230₺"), new("Kaşarlı Tost", "90₺"), new("Karışık Tost", "100₺"), new("Kaşarlı Omlet", "120₺"), new("Sade Omlet", "90₺"), new("Kaşar & 2 Çeşit Biberli Omlet", "120₺"), new("Pişi Tabağı", "130₺")]),
            new("Tatlılar", [new("Dilim Pasta Çeşitleri", "150₺"), new("Fırın Sütlaç", "100₺"), new("Baklava Porsiyon", "200₺"), new("Waffle (Çilek - Muz)", "170₺"), new("Külah Dondurma (Öğrenci Özel)", "50₺")]),
            new("İçecekler", [new("Kutu İçecekler", "50₺"), new("Soda", "25₺"), new("Meyveli Soda", "30₺"), new("Su", "15₺")])
        ]),
        new("Burgers Paket Mutfak", "+90-505-616-6321", "12:00-02:00",
        [
            new("Burgers", [new("MAT141", "320₺"), new("Cheese Burger", "320₺"), new("Statik", "320₺"), new("ENG101", "320₺"), new("Fizik 2", "320₺"), new("Klasik Hamburger", "320₺")]),
            new("Menüler", [new("MAT141 Menü", "370₺"), new("Cheese Burger Menü", "370₺"), new("Statik Menü", "370₺"), new("ENG101 Menü", "370₺"), new("Fizik 2 Menü", "370₺"), new("Klasik Hamburger Menü", "370₺")]),
            new("İçecekler", [new("Kola", "60₺"), new("Fanta", "60₺"), new("Sprite", "60₺"), new("Ayran", "60₺")])
        ]),
        new("Cafe İlla", "+90-539-657-0720", "12:00-02:00",
        [
            new("Hamburger", [new("Cheddar Burger", "350₺"), new("Klasik Burger", "330₺")]),
            new("Pizza", [new("Karışık", "350₺"), new("Dört Peynirli", "350₺"), new("Jambonlu", "350₺"), new("Margarita", "350₺"), new("Sucuklu", "350₺")]),
            new("Wrap", [new("Köfteli", "310₺"), new("Ton Balıklı", "270₺"), new("Cheddar Soslu Çıtır Tavuk", "240₺"), new("Ranch Soslu Çıtır Tavuk", "240₺")]),
            new("Makarna", [new("Penne Alfredo", "240₺"), new("Köfteli Spagetti", "310₺"), new("Mac & Cheese", "240₺"), new("Mac & Cheese Tavuk", "300₺"), new("Köri Soslu Penne Alfredo", "240₺"), new("Sosisli Spagetti", "300₺")]),
            new("Bazlama Tost", [new("Sucuklu", "220₺"), new("Karışık", "240₺"), new("Kaşarlı", "220₺"), new("Dört Peynirli", "240₺"), new("Tavuklu Bazlama Tost", "270₺"), new("Köfteli Bazlama Tost", "350₺")]),
            new("Kare Tost", [new("Kaşarlı", "220₺"), new("Dört Peynirli", "240₺"), new("Sucuklu", "220₺"), new("Karışık", "240₺")]),
            new("Izgaralar", [new("Izgara Köfte", "340₺"), new("Izgara Kanat", "310₺"), new("Porsiyon Patates", "200₺")]),
            new("Salata", [new("Sezar", "290₺"), new("Ton Balıklı", "310₺")]),
            new("Kahvaltı", [new("Mantarlı Cheddarlı Omlet", "240₺"), new("Sağanda Sucuklu Yumurta", "240₺"), new("Sağanda Kaşarlı Yumurta", "240₺"), new("Sağanda Sade Yumurta", "220₺"), new("Peynirli Su Böreği", "190₺")]),
            new("Tatlılar", [new("San Sebastian", "250₺"), new("Limon Cheesecake", "210₺"), new("Frambuaz Cheesecake", "210₺"), new("Orman Meyveli Cheesecake", "210₺"), new("Antep Mono", "230₺"), new("Oreo Mono", "220₺"), new("Mozaik", "210₺"), new("Tiramisu", "210₺"), new("Red Velvet", "210₺"), new("Gökkuşağı", "210₺"), new("Antep Rüyası", "230₺"), new("Kara Orman", "210₺"), new("Çikolatalı Profiterol", "210₺"), new("Pasta Latte", "210₺"), new("Devıs", "210₺"), new("Muzlu Malaga", "250₺"), new("Köstebek Pasta", "210₺"), new("Coco Star Pasta", "210₺"), new("Orman Meyveli Mono", "220₺"), new("3 Meyveli İlla Pasta", "290₺"), new("Waffle", "300₺"), new("İdeal Meyve", "219₺"), new("Föndü", "300₺"), new("Kağıt Helva", "60₺"), new("Dondurmalı İrmik Helvası", "250₺"), new("Cennet Çamuru", "270₺"), new("Cookie", "150₺"), new("Meyve Tabağı", "240₺"), new("Magnolya", "210₺"), new("Oreo Magnolya", "210₺"), new("Fırın Sütlaç", "210₺"), new("Sufle", "220₺"), new("Kazandibi", "210₺"), new("Browni", "230₺"), new("Süt Helvası", "210₺"), new("Traliçe", "220₺"), new("Spoonful", "210₺"), new("Saray Lokumu", "210₺"), new("Muzlu İbiza Tatlısı", "210₺"), new("Profiterol", "210₺"), new("Dondurmalar", "80₺")]),
            new("Sıcak İçecekler", [new("Sıcak Çikolata", "170₺"), new("Salep", "170₺"), new("Espressolu Sıcak Çikolata", "190₺"), new("Chai Tea Latte", "200₺"), new("Soğuk Çikolata", "180₺"), new("Soğuk salep", "180₺"), new("Soğuk Chai Tea Latte", "220₺")]),
            new("Soğuk İçecekler", [new("Su", "25₺"), new("Taze Sıkım Portakal Suyu", "150₺"), new("Limonata", "130₺"), new("Reyhan", "130₺"), new("Coca Cola", "90₺"), new("Coca Cola Zero", "90₺"), new("Pepsi", "90₺"), new("Fanta", "90₺"), new("Sprite", "90₺"), new("Niğde Gazoz", "90₺"), new("Fuse Tea Ananas", "90₺"), new("Fuse Tea Şeftali", "90₺"), new("Fuse Tea Limon", "90₺"), new("Fuse Tea Karpuz", "90₺"), new("Fuse Tea Kavun Çilek", "90₺"), new("Lipton Ice Tea Limon", "90₺"), new("Lipton Ice Tea Şeftali", "90₺"), new("Lipton Ice Tea Mango", "90₺"), new("Soda", "60₺"), new("Karpuz Çilek Soda", "70₺"), new("Limonlu Soda", "70₺"), new("Elmalı Soda", "70₺"), new("churchill", "120₺"), new("Ayran", "60₺"), new("Acılı Şalgam", "50₺"), new("Acısız Şalgam", "50₺")]),
            new("Milkshake", [new("Çikolatalı Milkshake", "240₺"), new("Çilekli Milkshake", "240₺"), new("Çikolatalı Fındıklı Milkshake", "240₺"), new("Sade Dondurmalı Milkshake", "240₺"), new("Karamelli Milkshake", "240₺"), new("Italian Karamelli Milkshake", "240₺"), new("Karadutlu Milkshake", "240₺"), new("Oreolu Milkshake", "240₺"), new("Portakallı Milkshake", "240₺"), new("Kavunlu Milkshake", "240₺"), new("Limonlu Milkshake", "240₺"), new("DamlaSakızlı Milkshake", "240₺"), new("Muzlu Milkshake", "240₺"), new("BalBadem Milkshake", "240₺")]),
            new("Frozen", [new("Yeşil Elma Frozen", "240₺"), new("Karpuz Frozen", "240₺"), new("Karadut Frozen", "240₺"), new("Çilek Frozen", "240₺"), new("Karpuz Çilek Frozen", "240₺"), new("Orman Meyveli Frozen", "240₺"), new("Muz Frozen", "240₺"), new("Mango Frozen", "240₺"), new("Kavun Frozen", "240₺"), new("Ejder Meyveli Frozen", "240₺"), new("Şeftali Frozen", "240₺"), new("Ananas Frozen", "240₺"), new("Nane limon Frozen", "240₺")]),
            new("Soğuk Çaylar", [new("Cool Lime", "210₺"), new("Mango Ananas", "210₺"), new("Hibiskus", "210₺"), new("Mango Ejder Meyvesi", "210₺"), new("Portakal Mango", "210₺"), new("Çilek Açai", "210₺"), new("YabanMersini Liçi", "210₺"), new("Çilek", "210₺"), new("Hibiskus OrmanMeyve", "210₺"), new("Kaktüs", "210₺"), new("Sakura BeyazŞeftali", "210₺"), new("Nar", "210₺")]),
            new("Çay", [new("Çay", "30₺"), new("Fincan Çay", "50₺"), new("kivi Oralet", "50₺"), new("portakal Oralet", "50₺"), new("yeşil elma Oralet", "50₺"), new("limon Oralet", "50₺")]),
            new("Bitki Çayları", [new("Yasemin", "160₺"), new("Papatya", "160₺"), new("Blue Butterfly", "180₺"), new("Melisa", "160₺"), new("Ihlamur", "180₺"), new("Yeşil Çay", "160₺"), new("Ekinezya", "160₺"), new("Ada Çayı", "160₺"), new("Winter Tea", "160₺"), new("Nanelimon", "160₺"), new("Rezene Çayı", "180₺")]),
            new("Türk Kahvesi", [new("Çikolatalı", "150₺"), new("Menengiç", "150₺"), new("Dibek", "150₺"), new("Dağ Çilekli", "150₺"), new("Türk Kahvesi", "130₺"), new("Damla Sakızlı", "150₺")]),
            new("Espressolu Kahve", [new("Cappuccino", "170₺"), new("Latte", "170₺"), new("Americano", "160₺"), new("Flat White", "180₺"), new("Cortado", "170₺"), new("Affogato", "190₺"), new("Espresso Macchiato", "180₺"), new("Karamel Macchiato", "190₺"), new("Mocha", "190₺"), new("White Chocolate Mocha", "190₺"), new("Espresso", "130₺"), new("Double Espresso", "150₺"), new("Espresso Con Panna", "180₺")]),
            new("Demleme Kahve", [new("Filtre Kahve", "160₺"), new("soğuk filtre kahve", "190₺"), new("COLD BREW", "230₺")]),
            new("Soğuk Kahveler", [new("Iced Latte", "210₺"), new("Iced Flat White", "220₺"), new("Iced Americano", "200₺"), new("Iced Caramel Machiato", "230₺"), new("Iced Caramel Latte", "230₺"), new("Iced Mocha", "230₺"), new("Iced White Mocha", "230₺")]),
            new("Nescafe", [new("Nescafe", "160₺"), new("Sütlü Nescafe", "180₺")]),
            new("Frappe", [new("Frappe", "230₺"), new("Caramel Frappe", "230₺"), new("Chocolate Frappe", "230₺"), new("White Chocolate Frappe", "230₺"), new("Vanilla Frappe", "230₺")])
        ]),
        new("Cafe Rien", "+90-850-308-0376", "08:00-22:00",
        [
            new("Kahvaltılıklar", [new("Kahvaltı Tabağı", "220₺"), new("Kaşarlı Tost", "185₺"), new("Üç Peynirli Tost", "215₺"), new("Sucuk ve Kaşarlı Tost", "240₺"), new("Mozarellalı Omlet", "190₺"), new("Mantarlı Omlet", "200₺"), new("Sucuklu Omlet", "300₺"), new("İspanyol Omleti", "210₺")]),
            new("Aperatifler", [new("Patates Kızartması", "105₺"), new("Soğan Halkası", "85₺"), new("Kızartma Tabağı", "245₺")]),
            new("Noodle ve Makarnalar", [new("HAYDOS İyte Menü", "-₺"), new("Penne Alfredo", "290₺"), new("Penne Napolitan", "190₺"), new("Spagetti Bolonez", "330₺"), new("Köri Mantarlı Penne", "290₺"), new("Ravigot Penne", "300₺"), new("Etli Noodle", "455₺"), new("Tavuklu Noodle", "310₺"), new("Sebzeli Noodle", "260₺")]),
            new("Burger ve Soğuk Sandwichler", [new("Cheese Burger", "340₺"), new("BBQ Mushroom Burger", "345₺"), new("Cheese-Onion Burger", "345₺"), new("Hamburger", "325₺"), new("Crispy Chicken Burger", "295₺"), new("Black Truffle", "355₺"), new("Dana Baconlu Soğuk Sandwich", "220₺"), new("Hindi Fümeli Soğuk Sandwich", "170₺")]),
            new("Quesadilla ve Wrapler", [new("Beef Wrap", "455₺"), new("Sebzeli Wrap", "255₺"), new("Çıtır Tavuklu Wrap", "320₺"), new("Izgara Tavuklu Wrap", "320₺"), new("Tavuklu Quesadilla", "330₺"), new("Beef Quesadilla", "470₺")]),
            new("Bowl ve Salatalar", [new("Ton Balıklı Salata", "355₺"), new("Kızarmış Tavuklu Salata", "315₺"), new("Izgara Tavuklu Salata", "315₺"), new("Sezar Salata", "315₺"), new("Cajun Soslu Tavuklu Salata", "295₺"), new("Baharatlı Hellim Salata", "330₺"), new("Izgara Orkinoslu Bowl", "365₺"), new("Kızarmış Tavuklu Bowl", "315₺"), new("Popcorn Chicken Bowl", "330₺")]),
            new("Tava Yemekleri", [new("Köri Mantarlı Tavuk", "330₺"), new("Schnitzel", "335₺"), new("Piliç Strogonoff", "340₺"), new("Falafel", "315₺"), new("Sweet & Sour", "330₺"), new("Colorado Chicken", "330₺"), new("Kartoffell", "335₺"), new("Thai Rice Chicken", "330₺"), new("General Tso's Chicken", "345₺"), new("Acılı Köfte", "375₺")]),
            new("Izgaralar", [new("Izgara Köfte", "320₺"), new("Izgara Tavuk", "300₺")]),
            new("Gratenler", [new("Enchilada", "300₺"), new("Lazanya Bolonez", "350₺"), new("Tavuk Graten", "330₺")]),
            new("Sıcak İçecekler ve Kahveler", [new("Çay", "-₺"), new("Bitki Çayları", "-₺"), new("Türk Kahvesi", "-₺"), new("Espresso", "-₺"), new("Double Espresso", "-₺"), new("Filtre Kahve", "-₺"), new("Cafe Americano", "-₺"), new("Cafe Latte", "-₺"), new("Flat White", "-₺"), new("Chocolate Mocha", "-₺"), new("White Chocolate Mocha", "-₺"), new("Cappucino", "-₺"), new("Iced Cafe Americano", "-₺"), new("Iced Cafe Latte", "-₺"), new("Iced Chocolate Mocha", "-₺"), new("Iced White Chocolate Mocha", "-₺"), new("Iced Cappucino", "-₺"), new("İlave Espresso Shot", "-₺"), new("İlave Şurup", "-₺")]),
            new("Soğuk İçecekler", [new("Kutu İçecekler", "-₺"), new("Ev Yapımı Limonata", "-₺"), new("Soda", "-₺"), new("Su", "-₺"), new("Ayran", "-₺"), new("Churchill", "-₺")]),
            new("Tatlılar", [new("Cheesecake", "-₺"), new("Tiramisu", "-₺"), new("Magnolia", "-₺")])
        ]),
        new("Caffe Luce", "+90-531-435-7077", "08:00-20:00",
        [
            new("Espresso Bazlı", [new("Espresso", "90₺"), new("Double Espresso", "100₺"), new("Espresso Macchiato", "100₺"), new("Espresso Con Panna", "100₺"), new("Cold Brew", "-"), new("Cortado", "140₺"), new("Americano", "130₺"), new("Latte", "145₺"), new("Cappuccino", "145₺"), new("Flat White", "155₺"), new("Coffee Mocha", "155₺"), new("Latte Caramel Macchiato", "165₺"), new("Viennese Coffee", "165₺"), new("Raspberry White Mocha", "175₺"), new("Strawberry Chocolate Mocha", "175₺"), new("Coconut Chocolate Mocha", "175₺"), new("Green Mint Chocolate", "175₺"), new("Triberry Coffee Latte", "175₺"), new("Peach Lotus Mocha", "175₺"), new("Orange Americano", "-")]),
            new("Filtre Kahve", [new("Filter Coffee", "130₺"), new("Decaf Filter Caffee", "140₺"), new("Hazelnut Filter Caffee", "150₺"), new("Raspberry Chocolate", "150₺"), new("Irish Creme", "150₺")]),
            new("Mix Ürünler", [new("Belgian White Mocha", "170₺"), new("Irish Creme Mocha", "170₺"), new("French Vanilla", "170₺"), new("Mexican Spiced Cocoa", "170₺"), new("Chocolate Raspberry Parfait", "170₺"), new("Cookies N'creme Crunch", "180₺"), new("Toasted Coconut Crunch", "180₺"), new("Sahlep", "155₺"), new("Damla Sakızlı Sahlep", "165₺"), new("Sıcak Çikolata", "155₺"), new("Chai tea latte", "170₺")]),
            new("Türk Kahvesi", [new("Türk Kahvesi", "90₺"), new("Double Türk Kahvesi", "120₺"), new("Damla Sakızlı Türk Kahvesi", "95₺")]),
            new("Luce Özel (Soğuk)", [new("Matilda Iced Turkish Coffee", "175₺"), new("Frappe Special", "175₺"), new("Babişkosunun Prensi", "175₺"), new("Lemango", "175₺"), new("Gurme Şirin", "175₺"), new("Limon Bulutu", "175₺"), new("Orman Meyveli Frozen", "175₺"), new("Triberry Frappe", "175₺"), new("Fresh Berry", "160₺"), new("Fresh Orange Mango", "160₺"), new("Fresh Lemon", "160₺"), new("Fresh Dragon Mango", "160₺"), new("Pink Magic", "160₺")]),
            new("Smoothie", [new("Mevsim Meyveli Smoothie", "195₺"), new("Milkshake", "180₺"), new("Tea Frozen", "170₺")]),
            new("Nitelikli Kahveler (Sıcak)", [new("Colombia Excelso Ep Narino", "175₺"), new("Colombia Excelso Ep Narino Dark Roast", "175₺"), new("Ethiopia Yirgacheffe Aricha GR. I", "175₺"), new("Honduras La Paz Marcala", "175₺")]),
            new("Nitelikli Kahveler (Soğuk)", [new("Colombia Excelso Ep Narino", "190₺"), new("Colombia Excelso Ep Narino Dark Roast", "190₺"), new("Ethiopia Yirgacheffe Aricha GR. I", "190₺"), new("Honduras La Paz Marcala", "190₺")]),
            new("Aromalı ve Bitkisel Çaylar", [new("Demleme Bardak Çay", "35₺"), new("Demleme Fincan Çay", "45₺"), new("TAW Çay", "100₺"), new("Christmas Tea", "105₺"), new("Grunchka Tea", "105₺"), new("Jasmine Green Tea", "105₺"), new("Apple & Cinnemon Tea", "105₺"), new("Winter Tea", "105₺"), new("Adaçayı", "105₺"), new("Ihlamur", "105₺"), new("Kuşburnu", "105₺"), new("Papatya", "105₺"), new("Naneli Yeşilçay", "105₺"), new("Nane Limon", "105₺"), new("Melisa", "105₺"), new("Hibiscus", "105₺"), new("Zencefil Limon", "105₺")]),
            new("Soğuk Meşrubatlar", [new("Su", "25₺"), new("Soda", "60₺"), new("Meyveli Soda", "70₺"), new("Kutu Cola", "90₺"), new("Luce Soda", "120₺"), new("Ice Tea", "90₺"), new("Churchill", "120₺"), new("Ev Yapımı Soğuk Çay", "120₺"), new("Uludağ limonata (400 ml buzlu bardak ile servis yapılır)", "90₺"), new("Cappy Portakal Suyu (400 ml buzlu bardak ile servis yapılır)", "90₺")]),
            new("Tatlılar", [new("Limonlu Cheesecake", "200₺"), new("Frambuazlı Cheesecake", "200₺"), new("Affogato", "190₺"), new("Çikolatalı Pasta", "210₺"), new("Tiramisu Cup Pasta", "180₺"), new("Lotus Cup Pasta", "180₺"), new("Red Velvet Cup Pasta", "180₺"), new("Dubai Cup Pasta", "250₺"), new("Dolgulu Donut", "110₺")]),
            new("Luce Mutfak", [new("Ton Bol Kaşar Eritme Sandwich", "265₺"), new("Kavurmalı Kaşarlı Sandviç", "265₺"), new("Sucuk Bol Kaşar Eritme Sandwich", "235₺"), new("Ciabatta Ekmeğine Luce Köfte", "250₺"), new("Kaşarlı Tost", "190₺"), new("Kaşar Sucuk Tost", "210₺"), new("Karışık Tost", "230₺"), new("Faruk Hoca Spesiyal Tost", "210₺"), new("Mantarlı Tost", "210₺"), new("Çok Karışık Tost", "290₺"), new("Yumurtalı Jambon Tost", "250₺"), new("Soğuk Sandviç Peynir", "200₺"), new("Ton Balıklı Soğuk Sandwich", "235₺"), new("Jambonlu Soğuk Sandwich", "235₺"), new("Hellim Sandwich", "255₺"), new("Avokadolu Hellim Sandwich", "275₺"), new("Yumurtalı Avocadolu Hellim Sandwich", "290₺"), new("Çok Acıktım Abi Ya Sandwich", "260₺"), new("Kahvaltı Tabağı", "320₺"), new("Omlet", "175₺"), new("Kaşarlı Omlet", "190₺"), new("Mantarlı Omlet", "215₺"), new("Mexican Omlet", "255₺"), new("Etli karışık omlet", "270₺")]),
            new("Ekstralar", [new("Büyük Boy Farkı", "20₺"), new("OATLY bitkisel süt", "40₺"), new("Extra Espresso Shot", "30₺"), new("Extra Şurup", "30₺")])
        ]),
        new("Extra Chicken House", "+90-543-110-6387", "11:00-23:00",
        [
            new("Tavuk Dürümler", [new("Tavuk Dürüm", "130₺"), new("Big Dürüm", "170₺"), new("Füze Dürüm", "170₺")]),
            new("Izgara Çeşitleri", [new("Adana Dürüm", "200₺"), new("Şiş Tavuk", "180₺"), new("Köfte Ekmek", "200₺")]),
            new("Izgara Porsiyonlar", [new("Adana", "260₺"), new("Şiş Tavuk", "250₺"), new("Köfte", "260₺")]),
            new("Tavuk Porsiyonlar", [new("Tavuk Porsiyon", "250₺"), new("Pilav Üstü", "230₺")]),
            new("Çıtır Menü", [new("Çıtır Schnitzel", "200₺"), new("Dolu Dolu Çıtır Sepeti", "230₺"), new("Dolu Dolu Çıtır Sepeti (2 Kişilik)", "400₺")]),
            new("Tavuk Pilav", [new("Tavuk Pilav (Ayran İkram)", "125₺"), new("1.5 Tavuk Pilav (Ayran İkram)", "180₺")]),
            new("İçecekler", [new("Su", "15₺"), new("Kutu İçecekler", "60₺"), new("Küçük Ayran", "20₺"), new("Büyük Ayran", "30₺"), new("Şalgam (Acılı / Acısız)", "45₺"), new("Sade Soda", "25₺"), new("Meyveli Soda", "35₺"), new("1 Lt Cola", "80₺"), new("2.5 Lt Cola", "110₺"), new("Cola / Fanta / Sprite", "45₺")])
        ]),
        new("Fabrika Kitchen", "+90-546-150-0001", "12:00-24:00",
        [
            new("Sporcu Menüler", [new("Sporcu Menü 1", "300₺"), new("Sporcu Menü 2", "330₺"), new("Sporcu Menü 3", "440₺")]),
            new("Aperatifler", [new("Patates kızartması", "140₺"), new("Elma dilim patates", "170₺"), new("Chedarlı patates", "210₺"), new("Çıtır tavuk", "290₺"), new("Soğan halkası", "190₺"), new("Mozarella stick", "240₺"), new("Çıtır sepet", "260₺"), new("Mega çıtır sepet", "340₺")]),
            new("Soğuk Salatalar", [new("Ton Balıklı Salata", "270₺"), new("Akdeniz Salata", "250₺"), new("Greek Salata", "220₺"), new("Mevsim Salata", "180₺")]),
            new("Sıcak Salatalar", [new("Tavuklu Sezar Salata", "270₺"), new("Izgara Tavuk Salata", "280₺"), new("Tavuk Salata", "250₺"), new("Çıtır Tavuk Salata", "270₺"), new("Çıtır peynirli salata", "270₺")]),
            new("Wrapler", [new("Tavuk Wrap", "250₺"), new("Et Wrap", "480₺"), new("Şinitzel Wrap", "280₺"), new("Köfte Wrap", "360₺"), new("Burrito", "280₺"), new("BBQ Wrap", "260₺"), new("Mexican Wrap", "270₺"), new("Sebzeli Wrap", "240₺")]),
            new("Quesadilla", [new("Tavuk Quesadilla", "340₺"), new("Et Quesadilla", "530₺")]),
            new("Izgaralar", [new("Izgara Köfte", "420₺"), new("Izgara Tavuk", "320₺")]),
            new("Tavalar", [new("Köri Soslu Tavuk", "320₺"), new("Teriyaki Soslu Tavuk", "340₺"), new("Mexican Soslu Tavuk", "340₺"), new("BBQ Soslu Tavuk", "330₺"), new("Kremalı Mantarlı Tavuk", "320₺"), new("ET TAVA", "570₺"), new("Sweet Chili Soslu Tavuk", "340₺")]),
            new("Makarnalar", [new("Penne Bolognese", "320₺"), new("Penne Arabiata", "230₺"), new("Alfredo Penne", "240₺"), new("Curry Penne", "250₺"), new("Pesto Penne", "260₺"), new("Sebzeli Penne", "230₺"), new("Penne Emiliano", "400₺"), new("Spagetti Bolognese", "340₺"), new("Spagetti Napoliten", "220₺"), new("Penne Napoliten", "200₺"), new("Ton Balıklı Penne", "280₺")]),
            new("Burgerler", [new("Chicken Burger", "250₺"), new("Hamburger", "340₺"), new("Cheese Burger", "380₺"), new("Füme Kaburga Burger", "410₺"), new("Fabrika Burger", "420₺"), new("BBQ Burger", "390₺"), new("Mushroom Burger", "420₺"), new("Mexican Burger", "400₺"), new("Dublex Burger", "570₺"), new("Fabrika Chicken Burger", "280₺"), new("Dublex Chicken Burger", "340₺")]),
            new("Pizzalar", [new("Margarita", "270₺"), new("Funghi", "290₺"), new("Pepperoni", "360₺"), new("Fabrika Mix", "350₺"), new("Füme Kaburga Pizza", "450₺"), new("Vejeteryan Pizza", "310₺"), new("Quattro Formaggi", "450₺"), new("Polo Pizza", "380₺"), new("Mexicano Pizza", "400₺"), new("Ton Balıklı Pizza", "360₺"), new("Pizza Turco", "530₺"), new("Şarküteri Pizza", "400₺"), new("Etli Pizza", "570₺"), new("Joe 1 PİZZA", "400₺"), new("Joe 2 Pizza", "400₺")]),
            new("İçecekler", [new("Su", "20₺"), new("Soda", "30₺"), new("Büyük Ayran", "50₺"), new("Coca Cola", "70₺"), new("Coca Cola Zero", "70₺"), new("Fanta", "70₺"), new("Sprite", "70₺"), new("Ice Tea (Şeftali, Mango, Limon)", "70₺"), new("Redbull/Monster", "90₺"), new("Litrelik Kola, Sprite, Fanta, Zero (Sadece Paket)", "80₺")])
        ]),
        new("Gülbahçe Pide", "+90-535-434-7553", "09:00-23:30",
        [
            new("Pide Çeşitleri", [new("Kıymalı Sade", "280₺"), new("Kıymalı Kaşarlı", "340₺"), new("Kıymalı Yumurtalı", "300₺"), new("Kuşbaşılı Sade", "350₺"), new("Kuşbaşılı Kaşarlı", "400₺"), new("Kuşbaşılı Kaşarlı Yumurtalı", "420₺"), new("Mantarlı", "300₺"), new("Domatesli Biberli", "300₺"), new("Sucuklu", "300₺"), new("Sucuklu Kaşarlı", "400₺"), new("Sucuklu Kaşarlı Yumurtalı", "420₺"), new("Ispanaklı Peynirli", "300₺"), new("Lahmacun", "250₺"), new("Kaşarlı Lahmacun", "300₺"), new("Fındık Lahmacun", "200₺"), new("Ispanaklı Yumurtalı", "320₺"), new("Sade Kaşarlı", "300₺"), new("Kaşarlı Yumurtalı", "320₺"), new("Mantarlı Kaşarlı Domatesli Biber Sucuklu", "370₺")]),
            new("Kebaplar", [new("Tavuk Şiş", "450₺"), new("Kanat", "450₺"), new("Adana Kebap", "450₺"), new("Urfa Kebap", "450₺"), new("Kiremitte Köfte", "450₺"), new("Kiremitte Tavuk", "450₺"), new("Kiremitte Kaşarlı Tavuk", "500₺"), new("Kiremitte Kaşarlı Köfte", "500₺")]),
            new("Çorbalar", [new("Kelle Paça", "280₺"), new("İşkembe", "280₺"), new("Mercimek", "180₺")]),
            new("Tatlılar", [new("Tahinli Pide", "400₺"), new("Künefe", "230₺")]),
            new("İçecekler", [new("Kola-Fanta-Gazoz", "85₺"), new("Ice Tea", "75₺"), new("Meyve Suyu", "75₺"), new("Ayran", "50₺"), new("Şalgam", "70₺"), new("Soda", "40₺"), new("Su", "25₺")])
        ]),
        new("Karnaval Gülbahçe", "+90-549-383-3535", "12:00-24:00",
        [
            new("Burger", [new("Kajun Burger", "299₺"), new("Cheeseburger", "319₺"), new("Smoked Burger", "399₺"), new("Double Cheeseburger", "419₺")]),
            new("Wrap", [new("Veggie Wrap (Vegan)", "249₺"), new("Falafel Wrap", "269₺"), new("Chicken Wrap", "289₺"), new("Crispy Chicken Wrap", "289₺"), new("Köfte Wrap", "309₺")]),
            new("Hotdog", [new("Classic", "179₺"), new("Newyorker", "209₺"), new("Crispy Dog", "219₺"), new("Dog with Bolognese", "309₺")]),
            new("Çıtırrr Kajun", [new("8'li", "319₺"), new("12'li", "419₺"), new("16'lı", "479₺")]),
            new("İçecek", [new("Coca Cola", "70₺"), new("Coca Cola Zero", "70₺"), new("Sprite", "70₺"), new("Fanta", "70₺"), new("Fuse Tea Şeftali", "70₺"), new("Fuse Tea Limon", "70₺"), new("Ayran", "50₺"), new("Soda", "50₺"), new("Su", "35₺")]),
            new("Box", [new("KING!!", "879₺")])
        ]),
        new("Keroz Truck", "+90-545-353-5311", "12:00-24:00",
        [
            new("Kömürde Sandviçler", [new("Klasik Karışık", "200₺"), new("Duble Karışık", "220₺"), new("Yumurtalı Karışık", "220₺"), new("Sayaslı Karışık", "220₺"), new("Sayaslı Yumurtalı Karışık", "240₺"), new("Pastırmalı Karışık", "240₺"), new("Keroz Karışık", "250₺"), new("Patso", "180₺")]),
            new("Kovada Waffle", [new("Kovada Waffle", "220₺")]),
            new("Bowl Çeşitleri", [new("Vejeteryan Bowl", "160₺"), new("Tavuklu Bowl", "200₺"), new("Ton Balıklı Bowl", "210₺"), new("Karışık Bowl", "220₺")]),
            new("İçecekler", [new("Coca-Cola", "70₺"), new("Coca-Cola Zero", "70₺"), new("Sprite", "70₺"), new("Ice Tea", "70₺"), new("Ayran", "40₺"), new("Soda", "30₺"), new("Su", "20₺")]),
            new("Yan Ürünler", [new("Patates Porsiyon", "150₺")])
        ]),
        new("KidonyaYENİ", "+90-537-778-4885", "09:00-21:00",
        [
            new("Burgerler ve Izgaralar", [new("Çıtır Burger", "275₺"), new("Çıtır Burger Menü", "375₺"), new("Hamburger Menü", "300₺"), new("Çıtır Tabak", "250₺"), new("Patates Kızartması", "200₺"), new("Izgara Köfte", "300₺")]),
            new("Salatalar", [new("Tavuklu Sezar Salata", "250₺"), new("Hellim Peynirli Salata", "240₺"), new("Çıtır Tavuk Salata", "250₺"), new("Tonlu Salata (75 gr)", "300₺")]),
            new("Makarnalar", [new("Kremalı Tavuklu", "200₺"), new("Soya Soslu Tavuklu", "200₺"), new("Napolitan", "200₺"), new("Körili Tavuklu", "220₺"), new("Pesto Soslu", "220₺"), new("Alfredo", "230₺"), new("Barbekü Soslu Tavuklu", "230₺"), new("Tonlu Mısırlı", "250₺"), new("Napolitan Menü (+ Kutu İçecek)", "250₺"), new("Et Dönerli Penne", "275₺")]),
            new("Tost ve Kumru", [new("Karışık Kumru", "140₺"), new("Karışık Kumru + Çay & Ayran (+10₺)", "150₺"), new("Ayvalık Tostu (Karışık)", "130₺"), new("Ayvalık Tostu (Karışık) + Çay & Ayran (+10₺)", "140₺"), new("Ayvalık Tostu (Kaşarlı)", "110₺"), new("Ayvalık Tostu (Kaşarlı) + Çay & Ayran (+10₺)", "120₺")])
        ]),
        new("Kimya Mühendisliği Kantin", "+90-541-297-2210", "07:30-21:30",
        [
            new("Makarnalar", [new("Alfredo", "215₺"), new("Mac Cheese", "215₺"), new("Ton Balıklı", "240₺"), new("Köri Soslu Tavuklu", "215₺"), new("Domates Soslu (Napoliten)", "150₺"), new("Special (Domates & Tavuk & Mantar", "250₺"), new("Çıtır Makarna", "275₺"), new("Arabiatta", "215₺")]),
            new("Salata", [new("Ton Balıklı Salata", "200₺"), new("Tavuk Sezar Salata", "200₺"), new("Çıtır Salata", "200₺"), new("Yarım Çıtır Salata", "160₺"), new("Izgara Köfte Salata", "250₺"), new("Mercimek Çorba", "75₺")]),
            new("Tavuk", [new("Tavuklu Pilav + Ayran", "150₺"), new("Çıtır Pilav", "235₺"), new("Çıtır Tavuklu Patates", "250₺"), new("Köfte Pilav", "250₺")]),
            new("Sandviç & Tost", [new("Kaşarlı Tost", "120₺"), new("Şnitzel Sandviç", "140₺"), new("Karışık Sandviç", "160₺"), new("Şnitzel Menü (Patates & Ayran)", "235₺"), new("Hamburger (Torku)", "175₺"), new("Hamburger Menü (Patates & Ayran)", "275₺"), new("Çıtır Dürüm Menü", "235₺"), new("Çıtır Dürüm", "160₺"), new("Çıtır Patates", "250₺"), new("Yarım Çıtır Patates", "160₺"), new("Köfte Ekmek + ayran", "220₺")]),
            new("Ev Yapımı Ürünler", [new("Islak Kurabiye", "30₺"), new("Pişi", "60₺"), new("Brownie", "45₺"), new("Tiramisu", "100₺"), new("Gül Böreği", "50₺")]),
            new("İçecekler", [new("KUTU KOLA 330 ML", "50₺"), new("FANTA 330 ML", "50₺"), new("SPRİTE 330 ML", "50₺"), new("FUSE TEA 330 ML", "50₺"), new("NİĞDE GAZOZU 250 ML", "40₺"), new("BEYOĞLU GAZOZU 250 ML", "50₺"), new("ZAFER GAZOZ 250 ML", "50₺"), new("BÜYÜK AYRAN", "30₺"), new("KÜÇÜK AYRAN", "20₺"), new("SADE SODA", "20₺"), new("MEYVELİ SODA", "30₺"), new("CAPRİ - SUN", "30₺"), new("PROTEİN SÜT", "80₺"), new("SU", "15₺"), new("PEPSİ 330 ML", "50₺"), new("MEYVE SUYU 200 ML", "30₺"), new("SIKMA MEYVE SUYU 250 ML", "70₺"), new("NESCAFE EXPRESS", "65₺")])
        ]),
        new("Köfteci Salih Usta", "+90-232-765-8135", "08:00-23:00",
        [
            new("Ekmek Arası", [new("Köfte Ekmek", "160₺"), new("Tavuk Ekmek", "160₺"), new("Ciğer Ekmek", "180₺"), new("Sucuk Ekmek", "160₺"), new("Sucuk Kaşar Yumurta", "180₺"), new("Sucuk Tavuk Köfte Ekmek", "180₺")]),
            new("Porsiyon", [new("Porsiyon Köfte", "300₺"), new("Porsiyon Tavuk", "300₺"), new("Porsiyon Ciğer", "350₺"), new("Karışık Porsiyon", "350₺")]),
            new("Menemen", [new("Sade Menemen", "200₺"), new("Sucuklu Kaşarlı Menemen", "220₺")]),
            new("İçecekler", [new("1 Litre Kola", "90₺"), new("2 Litre Kola", "100₺"), new("Şişe Kola", "60₺"), new("Kutu Kola", "80₺"), new("Şalgam", "50₺"), new("Su", "20₺"), new("Gazoz", "30₺"), new("Ayran", "30₺"), new("Ayran Büyük", "40₺")])
        ]),
        new("Komagene Etsiz Çiğ Köfte", "+90-542-491-8230", "12:00-01:30",
        [
            new("ATIŞTIRMALIKLAR", [new("ETİ BİDOLU", "35₺"), new("ETİ HOŞBEŞ FINDIKLI", "35₺"), new("ETİ HOŞBEŞ ÇİLEKLİ", "35₺"), new("ETİ HOŞBEŞ KAKAOLU", "35₺"), new("ETİ BROWNİ GOLD KEK", "30₺"), new("ETİ BROWNİ GOLD VİŞNELİ", "30₺"), new("ETİ BROWNİ FINDIKLI KEK", "30₺"), new("ÜLKER ALBENİ ÇİKOLATA", "30₺"), new("DORİTOS TACO BAHARATLI CİPS (MEGA PAKET)", "100₺")]),
            new("HAMBURGER", [new("OBURGENE", "125₺")]),
            new("ÇİĞ KÖFTE DÜRÜMLER", [new("Çiğ Köfte Dürüm (90gr)", "115₺"), new("Mega Dürüm (125gr)", "125₺"), new("Ultra Mega Dürüm (150gr)", "135₺"), new("Double Dürüm (175gr)", "145₺"), new("Doritoslu Dürüm", "130₺"), new("Doritoslu Mega", "145₺"), new("Doritoslu Ultra Mega", "155₺"), new("Doritoslu Double", "165₺")]),
            new("PORSİYONLAR", [new("Çiğ Köfte (200gr)", "185₺"), new("Çiğ Köfte (400gr)", "315₺"), new("Çiğ Köfte (600gr)", "440₺"), new("Çiğ Köfte (800gr)", "575₺"), new("Çiğ Köfte (1000gr)", "670₺")]),
            new("ÇİĞ KÖFTE TAKO MENÜLER", [new("Doritoslu Çiğ Köfte Taco Menü 1", "225₺"), new("Çiğ Köfte Taco Menü 2", "480₺"), new("İkili Taco Menü 3", "250₺")]),
            new("PİLAVLAR", [new("Nohutlu Tavuklu Pilav", "295₺"), new("Nohutlu Bol Tavuklu Pilav", "335₺"), new("Proteini Yüksek Tavuklu Pilav", "285₺")]),
            new("TATLILAR", [new("Şam Tatlısı", "65₺"), new("Fırın Sütlaç", "90₺"), new("Kazandibi", "90₺"), new("Kıbrıs Tatlısı (190gr)", "90₺"), new("Kadife Tatlısı (140gr)", "90₺"), new("Tavukgöğsü (180gr)", "90₺"), new("Supangle (180gr)", "90₺"), new("Profiterol (120gr)", "90₺"), new("Danette Çikolatalı Puding (100gr)", "60₺")]),
            new("İÇECEKLER", [new("Küçük  Ayran (170ml)", "35₺"), new("Büyük  Ayran (270ml)", "45₺"), new("Kızılay Sade Soda (200ml)", "25₺"), new("Kızılay Limonlu Soda (200ml)", "30₺"), new("Redbul Enerji İçeceği (250ml)", "100₺"), new("Lipton Ice Tea Şeftali (330ml)", "60₺"), new("Lipton Ice Tea Limonlu (330ml)", "60₺"), new("Fruko Gazoz (330ml)", "60₺"), new("Yedigün Portakal (330ml)", "60₺"), new("Pepsi Max (330ml)", "60₺"), new("Pepsi (330ml)", "60₺"), new("Erikli Su (500ml)", "20₺"), new("Pepsi (1L)", "100₺"), new("Pepsi Max (1L)", "100₺"), new("Yedigün Portakal (1L)", "100₺"), new("Fruko Gazoz (1L)", "100₺"), new("Komagene Şalgam Acılı ve Acısız (1L)", "75₺"), new("Komagene Ayran (1L)", "105₺")])
        ]),
        new("Kömürde Büfe", "+90-544-227-5816", "14:00-02:00",
        [
            new("Sandviçler", [new("İzmir Sandviç + Ayran", "170₺"), new("Yumurtalı İzmir + Ayran", "190₺"), new("Domatesli İzmir + Ayran", "180₺"), new("Patso + Ayran", "170₺"), new("Sucuk Ekmek + Ayran", "200₺")]),
            new("Hamburgerler", [new("Tek Köfte Hamburger + Ayran", "175₺"), new("Dauble Burger + Ayran", "200₺"), new("Tek Köfte Cheeseburger + Ayran", "200₺"), new("Dauble Cheeseburger + Ayran", "225₺")]),
            new("Tostlar", [new("Tek Kaşarlı Tost + Ayran", "150₺"), new("Çift Kaşarlı Tost + Ayran", "170₺"), new("Karışık Tost + Ayran", "170₺")]),
            new("İçecekler", [new("Kola (330 ml)", "60₺"), new("Kola Zero (330 ml)", "60₺"), new("Fanta (330 ml)", "60₺"), new("Sprite (330 ml)", "60₺"), new("Fuse Tea (330 ml) [Limon / Şeftali / Mango-Ananas]", "60₺"), new("Ayran", "30₺"), new("Su", "20₺"), new("Kola (1 lt)", "80₺"), new("Kola Zero (1 lt)", "80₺")])
        ]),
        new("Madam Rodona Restaurant Bistro & Pub", "+90-505-260-7932", "10:00-24:00",
        [
            new("KAMPANYALAR", [new("Bol Kıymalı Mantı (yoğurt + mantı sosu ile)", "135₺"), new("Ekmek Arası Izgara Köfte", "175₺"), new("Ekmek Arası Tavuk Kavurma", "150₺"), new("Kremalı Tavuklu Makarna", "150₺"), new("Köri Soslu Tavuklu Makarna", "160₺"), new("Köfteli Domates Soslu Makarna", "135₺"), new("Pesto Soslu Makarna", "135₺"), new("Tavuklu Pilav Bowl (pirinç pilavı + tavuk + mevsim salata)", "190₺"), new("Köfteli Pilav Bowl (soslu köfte + pirinç pilavı + mevsim salata)", "210₺"), new("Sebzeli Noodle", "200₺"), new("Tavuklu Noodle", "240₺"), new("Fit Menü 1: Izgara tavuk + tam buğday makarna + buharda sebze", "300₺"), new("Fit Menü 2: Chillı soslu tavuk + tam buğday makarna + buharda sebze", "300₺"), new("Fit Menü 3: Izgara tavuk + basmati pilav + yoğurt", "300₺"), new("Fit Menü 4: Izgara tavuk + basmati pilav + buharda sebze", "300₺"), new("Hamburger Menü (Patates Kızartması + 6'lı Soğan Halkası + 330ml Kola)", "290₺"), new("Cheeseburger Menü (Patates Kızartması + 6'lı Soğan Halkası + 330ml Kola)", "310₺"), new("Falafel Dürüm + Patates", "280₺")]),
            new("Kahvaltı", [new("SERPME KAHVALTI 2 Kişi", "1100₺"), new("AÇIK BÜFE(SINIRSIZ TEK KİŞİ)", "590₺"), new("EXPRESS KAHVALTI", "525₺"), new("MENEMEN", "250₺"), new("SUCUKLU YUMURTA", "300₺"), new("SAHANDA GÖZ YUMURTA", "200₺"), new("SAHANDA SUCUK", "325₺"), new("AVAKADOLU ÇITIR EKMEK", "420₺")]),
            new("Aperatifler", [new("FRENCH FRIED PATATOES", "200₺"), new("TORI KARAAGE", "350₺"), new("MIXED APPERATIVE PLATE", "480₺"), new("CHEDDARLI ANTRİKOT", "550₺"), new("ORLİ CİĞER", "550₺"), new("ŞARAP TABAĞI", "575₺"), new("SİGARA BÖREĞİ", "250₺"), new("GÜVEÇTE KAŞARLI MANTAR", "320₺")]),
            new("Salatalar", [new("MADAM RODONA SALAD", "400₺"), new("FALAFEL BOWL", "400₺"), new("ILIK IZGARA BONFILE", "475₺"), new("HORITAKI SALAD", "330₺")]),
            new("Sandviçler & Dürümler", [new("TAVUKLU BAZLAMA SANDVİÇ", "425₺"), new("ETLİ BAZLAMA SANDVİÇ", "525₺"), new("BEEF WRAP", "475₺"), new("CHICKEN WRAP", "375₺"), new("Falafel Dürüm + Patates", "280₺")]),
            new("Makarnalar", [new("FETTUCINI ALFREDO", "325₺"), new("TAVUK VE MANTARLI FETTUCINI", "375₺"), new("MACARONI AND CHEESE", "390₺")]),
            new("Pizzalar", [new("ITALIANO PIZZA", "400₺"), new("PEPPERONI PIZZA", "425₺"), new("STUFFED PIZZA", "450₺"), new("BBQ SPECIAL PIZZA", "460₺"), new("TANDIR PİZZA", "575₺"), new("PIZZA LOKAL", "425₺"), new("KABURGA PIZZA", "550₺")]),
            new("Burgerler", [new("KLASIK BURGER", "390₺"), new("YELLOW ROSE BURGER", "420₺"), new("RODONA BURGER", "520₺"), new("KING BURGER", "550₺"), new("TANDIR BURGER", "590₺"), new("CHICKEN BURGER", "375₺"), new("VİŞNELİ TULUM BURGER", "450₺"), new("BEĞENDİLİ BURGER", "460₺")]),
            new("Meze ve Ara Sıcaklar", [new("ŞAKŞUKA", "200₺"), new("BALKAN MEZE", "200₺"), new("TARHUNLU KURU CACIK", "200₺"), new("ZEYTİN SALATALI HUMUS", "220₺"), new("GİRİT EZME", "220₺"), new("SÖĞÜŞ TABAĞI", "300₺")]),
            new("Ana Yemekler", [new("ISLAMA KASAP KÖFTE", "575₺"), new("BEEF STRAAGANOFF", "650₺"), new("SÖĞÜRTMELİ DANA", "675₺"), new("MEKSİKA SOSLU TAVUK", "525₺"), new("DUXELLES TAVUK", "550₺"), new("TANDIR LOKUM", "710₺"), new("GÜVEÇTE KOKOREÇ PORSİYON", "390₺")]),
            new("Tatlılar", [new("MADAM'S ROSES", "375₺"), new("FLAMENCO FRUIT CUP", "350₺"), new("PANNA COTTA", "190₺"), new("SUFFLE", "280₺"), new("APPLE CRUMBLE", "300₺")]),
            new("Soğuk İçecekler", [new("REDBULL", "130₺"), new("COCA COLA", "90₺"), new("SPRITE", "90₺"), new("CAPPY (VİŞNE/ŞEFTALİ/KARIŞIK)", "90₺"), new("FUSE TEA", "90₺"), new("CHURCHİL", "90₺"), new("SODA", "60₺"), new("AYRAN", "45₺"), new("SU", "35₺"), new("ICE LATTE", "135₺"), new("COLD BREW", "140₺"), new("LIMONATA", "120₺"), new("MOJITO", "145₺")]),
            new("Sıcak İçecekler", [new("DEMLEME ÇAY", "40₺"), new("TÜRK KAHVESİ TEK", "60₺"), new("TÜRK KAHVESİ DOUBLE", "95₺"), new("ESPRESSO", "100₺"), new("DOUBLE ESPRESSO", "145₺"), new("SALEP", "150₺"), new("SICAK ÇİKOLATA", "130₺")]),
            new("Fit Menü", [new("Fit Menü 1", "300₺"), new("Fit Menü 2", "300₺"), new("Fit Menü 3", "300₺"), new("Fit Menü 4", "300₺")])
        ]),
        new("Matrixx Fried Chicken", "+90-505-616-6321", "12:00-02:00",
        [
            new("Burgerler", [new("Matrixx Burger + Patates", "240₺"), new("Focus Burger + Patates", "240₺"), new("Truffle Burger + Patates", "240₺"), new("Basic Burger + Patates", "240₺"), new("Dynamite Burger + Patates", "240₺"), new("Dream Burger + Patates", "240₺"), new("Midnight Burger + Patates", "240₺"), new("Double Burger", "310₺")]),
            new("Soslar", [new("Slash Sos", "20₺"), new("Dynamite Sos", "20₺"), new("Ranch Sos", "20₺"), new("Truffle Sos", "20₺"), new("Rock Sos", "20₺")]),
            new("İçecekler", [new("Kola", "70₺"), new("Kola Zero", "70₺"), new("Fanta", "70₺"), new("Küçük Ayran", "25₺"), new("Büyük Ayran", "35₺"), new("Sprite", "70₺"), new("Ice Tea", "70₺")])
        ]),
        new("Maviş Ev Yemekleri", "+90-542-765-7006", "08:00-20:00",
        [
            new("Arnavut Ciğeri menü", [new("3 çeyrek ciğer +ayran", "220₺"), new("Pilav üstü ciğer +ayran", "280₺"), new("Yarım Ekmek ciğer +ayran", "180₺")]),
            new("Güncel kampanyalar", [new("Çorba +yarım tavuk Döner", "250₺"), new("Çorba +3 Çeyrek tavuk Döner", "300₺"), new("Çorba +pilav üstü tavuk döner", "350₺"), new("Çorba +3 Çeyrek et döner", "430₺"), new("Çorba +pilav üstü et döner", "500₺"), new("3 Çeyrek Arnavut ciğeri", "200₺"), new("Çorba +yarım ekmek ciğer", "250₺"), new("Yarım Ekmek Arnavut ciğeri +ayran", "180₺")]),
            new("İçecekler", [new("Kutu içecekler", "80₺"), new("Büyük ayran", "40₺"), new("Su(0,5)", "20₺"), new("Soda", "40₺"), new("Şalgam suyu", "50₺"), new("Niğde gazozu", "50₺"), new("Türk kahvesi", "50₺"), new("Nescafe", "50₺"), new("Kola(1lt)", "100₺"), new("Kola(2,5 lt)", "150₺"), new("Küçük Ayran", "30₺"), new("Kola, Fanta, Sprite, Fuse Tea (Kutu)", "80₺")]),
            new("Kahvaltı menü", [new("Kahvaltı tabağı", "300₺"), new("Serpme kahvaltı", "400₺"), new("Sucuklu yumurta", "180₺"), new("Melemen", "180₺"), new("Karışık tost", "170₺"), new("Sucuklu tost", "150₺"), new("Kaşarlı tost", "150₺"), new("Günün tatlısı", "50₺"), new("Kıymalı melemen", "180₺"), new("kaşarlı Omlet", "180₺")]),
            new("Yemek & Çorbalar", [new("Mercimek Çorbası (Öğrenci = 130₺)", "150₺"), new("Kelle Paça Çorbası(öğrenci 230)", "250₺"), new("Ev Mantısı", "250₺"), new("Et Döner (Yarım Ekmek)", "250₺"), new("Et Döner (3 Çeyrek) öğrenci 300", "350₺"), new("Et Döner (Pilav Üstü) öğrenci 400", "450₺"), new("Tavuk Döner (Yarım Ekmek) öğrenci 150", "150₺"), new("Tavuk Döner (3 Çeyrek) öğrenci 200", "220₺"), new("Tavuk Döner (Pilav Üstü) öğrenci 250", "300₺"), new("Karışık Tost", "180₺")])
        ]),
        new("Mellon Coffee", "+90-532-416-8924", "10:00-01:00",
        [
            new("Sıcak Kahveler", [new("Single Espresso", "80₺"), new("Double Espresso", "90₺"), new("Filtre Kahve", "115₺"), new("Americano", "115₺"), new("Cappucino", "130₺"), new("Coffee Latte", "130₺"), new("Lungo", "95₺"), new("Cortado", "120₺"), new("Flat White", "135₺"), new("Macchiato", "135₺"), new("Caramel Macchiato", "150₺"), new("Mocha", "165₺"), new("White Mocha", "165₺"), new("Caramel Mocha", "170₺"), new("Pumpkin Spice Latte", "160₺"), new("Almond Latte", "160₺"), new("Chai Tea Latte", "180₺"), new("Salted Caramel Latte", "160₺"), new("Hot Chocolatte", "150₺")]),
            new("Soğuk Kahveler", [new("Ice Americano", "125₺"), new("Ice Coffee Latte", "140₺"), new("Ice Flat White", "145₺"), new("Ice Caramel Macchiato", "160₺"), new("Ice Mocha", "175₺"), new("Ice White Mocha", "175₺"), new("Ice Caramel Mocha", "180₺"), new("Ice Pumpkin Spice Latte", "175₺"), new("Ice Almond Latte", "175₺"), new("Ice Chai Tea Latte", "185₺"), new("Ice Salted Caramel Latte", "165₺"), new("Cold Brew", "150₺"), new("Cold Brew Latte", "170₺"), new("Ice Frappe", "150₺")]),
            new("3RD Wave", [new("Chemex", "165₺"), new("V60", "165₺")]),
            new("Frozen", [new("Mango", "170₺"), new("Çilek", "170₺"), new("Orman Meyveli", "170₺"), new("Karadut", "170₺"), new("Böğürtlen", "170₺")]),
            new("İçecekler", [new("Su", "30₺"), new("Çay", "40₺"), new("Bitki Çayları", "100₺"), new("Sahlep", "150₺"), new("Türk Kahvesi", "80₺"), new("Soda", "50₺"), new("Meyveli Soda", "55₺"), new("Churchill", "90₺"), new("Gazoz", "70₺"), new("Cola, Fanta, Sprite", "70₺"), new("Meyve Suyu", "60₺"), new("Redbull", "100₺"), new("Limonata", "110₺")]),
            new("Refresha", [new("Cool Lime", "150₺"), new("Berry Hibiscus", "150₺"), new("Watermelon Lemonade", "150₺")]),
            new("Pasta", [new("Limonlu Cheesecake", "150₺"), new("Frambuazlı Cheesecake", "150₺"), new("Lotuslu Cheesecake", "150₺"), new("Browni Üç Çikolatalı", "250₺"), new("San Sebastian", "165₺"), new("Spekülas Dome", "200₺"), new("Pasta Latte", "160₺"), new("Frambuazlı Çikolatalı", "200₺"), new("Yaban Mersinli Krepli", "180₺"), new("Çikolatalı Cheesecake", "165₺")]),
            new("İlaveler", [new("Süt", "20₺"), new("Laktozsuz", "15₺"), new("Badem Sütü", "25₺"), new("Extra Shot", "25₺"), new("Extra Şurup", "25₺")])
        ]),
        new("MoccaSin", "+90-532-769-0690", "11:00-01:00",
        [
            new("Izgara", [new("isveç soslu tavuk", "355₺"), new("kremalı tavuk", "355₺"), new("körili tavuk", "355₺"), new("şnitzel", "315₺"), new("Fajita", "315₺"), new("ekstra lavaş", "20₺")]),
            new("Wrap", [new("Çıtır Tavuk Wrap", "345₺"), new("ızgara Tavuk wrap", "345₺"), new("Ton Balıklı wrap", "295₺"), new("Pesto Wrap", "235₺"), new("Vejeteeyan Wrap", "195₺")]),
            new("Sandaviç", [new("Pesto Sandviç", "195₺"), new("Füme Sandviç", "215₺"), new("Ton Sandviç", "285₺")]),
            new("Burger", [new("Cheese Burger", "385₺"), new("Hot Pepper Burger", "395₺"), new("Mushroom Burger", "435₺"), new("Dana Bacon Burger", "455₺"), new("Chicken Burger", "315₺"), new("Bütün burgerler patates ile servis edilmektedir.", "-")]),
            new("Pizza", [new("Margarita", "345₺"), new("Pepperoni", "375₺"), new("Ala Turka", "385₺"), new("Buffalo", "465₺"), new("Acımasız Ton", "395₺"), new("Quattro Formaggi", "445₺"), new("Vejeteryan", "395₺"), new("Mantarlı", "365₺"), new("Oberjin", "385₺"), new("Hindişko", "395₺"), new("Kebab Pizza", "395₺"), new("Krema Pesto", "415₺"), new("PİZZALARDA PAKET SERVİSİMİZ YOKTUR", "-")]),
            new("Makarna", [new("Calipso", "345₺"), new("Napoliten", "215₺"), new("Bolonez", "315₺"), new("Alfredo", "345₺"), new("Poseıdon", "345₺")]),
            new("Tavuk & Aperatif", [new("Crispy Chicken Parçaları", "290₺"), new("Fırında Çıtır Patates", "185₺"), new("Trüf Parmesanlı Patates", "225₺"), new("Çıtır Bira Tabağı", "300₺"), new("Turşu", "50₺"), new("İsveç Soslu Patates", "225₺"), new("Yoğurt Soslu Pataes", "225₺"), new("Cedar Soslu Patates", "225₺"), new("Domates Soslu Ptates", "225₺")]),
            new("Salata", [new("Beyaz Peynirli Salata", "315₺"), new("Ton Balıklı Salata", "315₺"), new("Çıtır Tavuklu Salata", "335₺"), new("Hellim Peynirli Salata", "335₺")]),
            new("Tatlı", [new("Magnolya", "145₺"), new("Supangle", "145₺"), new("Tiramisu", "185₺"), new("Browni", "200₺"), new("Çikolata Parçacıklı Kurabiye", "90₺")]),
            new("Kahve", [new("Espresso", "90₺"), new("Double Espresso", "105₺"), new("Americano", "150₺"), new("Cappucino", "170₺"), new("Mocha", "170₺"), new("White Mocha", "170₺"), new("Latte", "160₺"), new("Caramel Latte", "170₺"), new("Lotus Latte", "180₺"), new("Irish Latte", "175₺"), new("Toffe Nut", "180₺"), new("Salted Caramel", "170₺"), new("Flat White", "170₺"), new("Filtre Kahve", "150₺"), new("Vanilla Latte", "170₺"), new("Türk Kahvesi", "75₺"), new("Caramel Mocha", "170₺"), new("Pumpkin Spice Latte", "175₺")]),
            new("Kahve Ekstraları", [new("Badem Sütü", "50₺"), new("Yulaf Sütü", "50₺"), new("Extra Shot", "30₺"), new("Extra Şurup", "30₺")]),
            new("Çay", [new("Çay Bardak", "25₺"), new("White Jasmine", "100₺"), new("White Roseberry", "100₺"), new("Daydream", "100₺"), new("Chai Masala", "100₺"), new("Berry Punch", "100₺"), new("Rooibos Vanilla", "100₺"), new("Rooibos Yılbaşı Harmanı", "100₺"), new("Winter Tea", "100₺"), new("Love Blend", "100₺"), new("Detox", "100₺"), new("Green Glow", "100₺"), new("Milk Oolong", "100₺"), new("Peach Blossom Oolong", "100₺"), new("Turmeric", "100₺")]),
            new("Matcha", [new("Matcha Latte", "180₺"), new("Dirty Matcha Latte", "210₺"), new("Strawberry Matcha Latte", "220₺"), new("Mango Matcha Latte", "220₺"), new("Matcha Lemonade", "220₺")]),
            new("Soğuk İçecekler", [new("Cool Lime", "110₺"), new("Berry Hibiscus", "110₺"), new("Blueberry Lychee", "110₺"), new("Limonata", "120₺"), new("Çilekli Limonata", "120₺"), new("Churchill", "65₺")]),
            new("Diğer İçecekler", [new("Sıcak Çikolata", "120₺"), new("Salep", "120₺"), new("Kutu İçecekler", "75₺"), new("Maden Suyu", "40₺"), new("Su", "20₺"), new("Redbull", "100₺")])
        ]),
        new("Nazilli Pide Salonu", "+90-537-780-3509", "08:00-02:00",
        [
            new("Çorba Çeşitleri", [new("Mercimek", "200₺"), new("Ezogelin", "200₺"), new("Tavuk Suyu", "200₺"), new("İşkembe", "300₺"), new("Kellepaça", "300₺"), new("Beyin", "300₺"), new("Ayakpaça", "300₺"), new("Karışık", "320₺"), new("Full Karışık", "340₺")]),
            new("Pide Çeşitleri", [new("Kıymalı Pide", "320₺"), new("Kıymalı Yumurtalı", "340₺"), new("Kıymalı Kaşarlı", "390₺"), new("Kuşbaşılı Sade", "450₺"), new("Kuşbaşılı Kaşarlı", "520₺"), new("Kuşbaşılı Yumurtalı", "470₺"), new("Kavurmalı Sade", "550₺"), new("Kavurmalı Kaşarlı", "620₺"), new("Kaşarlı Sade", "320₺"), new("Kaşarlı Yumurtalı", "350₺"), new("Kaşarlı Sucuklu", "450₺"), new("Kaşarlı Mantarlı", "350₺"), new("Sucuklu Kaşarlı Yumurtalı", "470₺"), new("Special Pide", "620₺"), new("Pastırmalı Kaşarlı", "650₺"), new("Tavuklu Sade", "400₺"), new("Tavuklu Kaşarlı", "470₺"), new("Sebzeli Pide", "320₺"), new("3 Peynirli Pide", "350₺"), new("Ispanaklı Peynirli", "350₺"), new("Naturel (Sade Kıyma)", "500₺"), new("Naturel Kaşarlı (Sade Kıyma + Kaşar)", "570₺"), new("Karışık Pide", "550₺"), new("Sebzeli Kaşarlı Pide", "350₺"), new("Vegan Pide", "320₺")]),
            new("Lahmacun Çeşitleri", [new("Fındık Lahmacun", "200₺"), new("Lahmacun", "320₺"), new("Antep Lahmacun", "380₺"), new("Antep Lahmacun Kaşarlı", "430₺"), new("Kıymalı Kaşarlı Lahmacun", "370₺"), new("Nazilli Gülü Porsiyon", "480₺"), new("Kuşbaşı Kaşarlı Lahmacun", "450₺")]),
            new("Kebap Çeşitleri", [new("Adana (Acılı)", "550₺"), new("Urfa", "550₺"), new("Tavuk Şiş İncik", "550₺"), new("Tavuk Kanat Şiş", "550₺"), new("Izgara Köfte", "550₺"), new("Beyti Sarma", "630₺"), new("Adana İskender", "580₺"), new("Urfa İskender", "580₺"), new("Patlıcan Kebap", "650₺"), new("Manisa Kebap", "580₺"), new("Pirzola", "950₺"), new("Bonfile", "-"), new("Antrikot", "800₺"), new("Et Şiş", "900₺"), new("Ciğer Şiş", "550₺"), new("Ciğer Tava", "480₺"), new("Kuzu Çöp Şiş", "750₺"), new("Kuzu Kaburga", "-"), new("Izgara Kaşarlı Köfte", "630₺"), new("Karışık Izgara", "1400₺"), new("Vali (2 Kişilik)", "2900₺"), new("Nazilli Special (4-6 Kişilik)", "5800₺"), new("Çocuk Menü", "450₺")]),
            new("Tava Çeşitleri", [new("Sac Kavurma", "750₺"), new("Kilis Tava", "600₺"), new("Piliç Tava", "600₺"), new("Kurban Kavurma", "750₺")]),
            new("Kiremit Çeşitleri", [new("Kiremitte Köfte", "550₺"), new("Kiremitte Kaşarlı Köfte", "600₺"), new("Kiremitte Tavuk", "550₺"), new("Kiremitte Kaşarlı Tavuk", "600₺"), new("Kiremitte Mantar Kaşar", "500₺")]),
            new("Pizza Çeşitleri", [new("Sebzeli Pizza", "420₺"), new("Karışık Pizza", "500₺"), new("Sucuklu Pizza", "470₺"), new("Pastırmalı Pizza", "750₺"), new("Kavurmalı Pizza", "750₺"), new("Ton Balıklı Pizza", "550₺"), new("Nazilli Special Pizza", "800₺"), new("Patates Cips Porsiyon", "200₺")]),
            new("Kahvaltı", [new("Kahvaltı Tabağı", "-"), new("Serpme Kahvaltı", "-"), new("Menemen", "300₺"), new("Menemen Kaşar Sucuklu", "350₺"), new("Menemen Kaşarlı", "300₺"), new("Sucuklu Yumurta", "300₺"), new("Yumurta Sahanda", "250₺"), new("Omlet", "250₺"), new("Omlet Kaşarlı", "300₺"), new("Pastırmalı Yumurta", "400₺"), new("Kavurmalı Yumurta", "400₺"), new("Pişi", "-"), new("Haşlanmış Yumurta", "-")]),
            new("Salata Çeşitleri", [new("Çoban Salata", "250₺"), new("Mevsim Salata", "250₺"), new("Peynirli Salata", "300₺"), new("Gavurdağ Salata", "400₺"), new("Tavuklu Salata", "550₺"), new("Ton Balıklı Salata", "500₺")]),
            new("Tatlı Çeşitleri", [new("Kara Fırın Künefe", "250₺"), new("Kara Fırın Katmer", "300₺"), new("Fıstıklı Kadayıf", "300₺"), new("Tahinli Pide (Ballı Cevizli)", "500₺"), new("Çikolatalı Pide", "250₺"), new("Sütlaç", "170₺"), new("Kemalpaşa", "150₺"), new("İlave Kaymak", "80₺"), new("İlave Dondurma", "100₺")]),
            new("İçecek Çeşitleri", [new("Kutu Kola", "100₺"), new("Kutu Fanta", "100₺"), new("Kutu Sprite", "100₺"), new("Fuse Tea", "100₺"), new("Meyve Suyu", "100₺"), new("Şalgam", "70₺"), new("Yayık Ayran", "70₺"), new("Şişe Ayran", "70₺"), new("Niğde Gazozu", "70₺"), new("Soda", "40₺"), new("Su", "25₺")])
        ]),
        new("Ohannes Burger", "+90-232-754-5040", "",
        [
            new("Burgerler", [new("Cheese Burger", "365₺"), new("Mexi Burger", "355₺"), new("Smash Burger", "390₺"), new("BBQ Burger", "355₺"), new("Smash Cheese Burger", "395₺"), new("Smash Spicy Burger", "375₺"), new("Mushroom Burger", "370₺"), new("Bifteks Burger", "405₺"), new("Classic Burger", "330₺"), new("Big Lebowski", "375₺"), new("Cheesy Beast Burger", "515₺"), new("Smokehouse Burger", "560₺"), new("Smokehouse Double Smash Burger", "600₺"), new("Lokum Burger Yeni", "510₺"), new("Crispy Chicken Burger", "310₺"), new("Chicken Burger", "300₺"), new("Tandır Burger", "510₺"), new("Golden Chicken Burger Yeni", "220₺"), new("Kebap Burger Yeni", "495₺"), new("Buffalo Chicken Burger", "330₺"), new("Veggie Burger", "290₺"), new("Chicken Burger Kids", "300₺"), new("Classic Burger Kids", "350₺")]),
            new("Aperatifler", [new("Texas Tavuk", "220₺"), new("Cheesy Bites", "220₺"), new("Jalapeno Poppers Bites", "210₺"), new("Chicken Tenders", "250₺"), new("Patates Kızartması", "100₺"), new("Soğan Halkası 6lı", "70₺"), new("El Yapımı Soğan Halkası 6lı", "190₺"), new("Mozzarella Sticks 4lü", "160₺")]),
            new("İçecekler", [new("Sütaş Kutu Ayran 300ml", "45₺"), new("Litrelik İçecek 1000ml", "160₺"), new("Şişe İçecek 300ml", "70₺"), new("Uludağ Limonata 250ml", "80₺"), new("Uludağ Gazoz 250ml", "60₺"), new("Kutu İçecek 330ml", "95₺"), new("Uludağ Portakallı Gazoz 250ml", "65₺"), new("Soda Cam", "60₺"), new("Su", "30₺")]),
            new("Menüler", [new("Bilader Menü", "750₺"), new("Oha Box", "980₺"), new("Lunch Box", "485₺"), new("Bao Bun 2li", "660₺")])
        ]),
        new("Opera Coffee", "", "08:00-01:00",
        [
            new("Kampanyalar", [new("Filtre Kahve + Kruvasan", "150₺")]),
            new("Sıcak Kahve", [new("Americano", "-"), new("Filtre Kahve", "-"), new("Sütlü Filtre Kahve", "-"), new("Latte", "-"), new("Cappuccino", "-"), new("Opera Montblanc", "-"), new("Caramel Latte", "-"), new("Vanillia Latte", "-"), new("Pumpkin Spice Latte", "-"), new("Hazelnut Latte", "-"), new("Salted Caramel Latte", "-"), new("Dark Chocolate Mocha", "-"), new("White Chocolate Mocha", "-"), new("Irish Coffee", "-"), new("Golden Bite", "-"), new("Türk Kahvesi", "-"), new("Espresso", "-"), new("Flat White", "-"), new("Cortado", "-")]),
            new("Sıcak İçecek", [new("Sıcak Çikolata", "-"), new("Beyaz Çikolata", "-"), new("Sahlep", "-"), new("Chai Tea Latte", "-"), new("Caramel Dusk", "-"), new("Ginger Radience", "-"), new("White Zen", "-"), new("Matcha Latte", "-")]),
            new("Soğuk Kahveler", [new("Iced Americano", "-"), new("Iced Latte", "-"), new("Iced Caramel Latte", "-"), new("Iced Vanillia Latte", "-"), new("Iced Filtre Kahve", "-"), new("Iced Salted Caramel", "-"), new("Iced Oreo Latte", "-"), new("Iced Lotus Biscoff", "-"), new("Iced Caramel Macchiato", "-"), new("Iced Cappuccino", "-"), new("Iced Dark Chocolate Mocha", "-"), new("Iced Latte Lavanta", "-"), new("Iced Flat White", "-"), new("Iced White Chocolate Mocha", "-"), new("Cold Brew", "-"), new("Iced Sütlü Filtre Kahve", "-")]),
            new("Soğuk İçecekler", [new("Fresh Lavender", "-"), new("Citrimelon", "-"), new("Mango Inferno", "-"), new("Citrus Bloom", "-"), new("Red Yuzu Spark", "-"), new("Raspberry Breeze", "-"), new("Iced Strawberry Matcha", "-"), new("Iced Matcha Latte", "-"), new("Ev Yapımı Limonata", "-"), new("Çilekli Limonata", "-"), new("Iced White Chocolate", "-"), new("Iced Chai Tea Latte", "-"), new("Iced Chocolate", "-")]),
            new("Tatlı", [new("Leydi Vişneli Kek", "-"), new("San Sebastian", "-"), new("Üç Çikolatalı Brownie", "-"), new("Limonlu Cheesecake", "-"), new("Frambuazlı Cheesecake", "-"), new("Polka", "-"), new("Strawberry Filled Donut", "-"), new("Monalisa", "-"), new("Frambuazlı Purple Pasta", "-"), new("Tiramisu", "-"), new("Mozaik Pasta", "-"), new("Vanillia Ice", "-"), new("Cream Filled Donut", "-"), new("Chocolate Donut", "-")]),
            new("Kruvasan", [new("Tereyağlı Kruvasan", "-"), new("Peynirli Çörek", "-"), new("Çikolatalı Kruvasan", "-")]),
            new("Sandviç", [new("Kremalı Tavuklu", "-"), new("Dört Peynirli Foccacia", "-"), new("Hellim Peynirli", "-"), new("Hindi Füme Panini", "-"), new("Dana Jambonlu Tahıllı", "-"), new("Artizan Mozerella Sandviç", "-"), new("Artizan Tavuklu Sandviç", "-")]),
            new("Börekler", [new("Peynirli Rulo Börek", "-"), new("Patatesli Rulo Börek", "-"), new("Ispanaklı Rulo Börek", "-")])
        ]),
        new("Orivhi", "+90-545-915-7741", "12:00-24:00",
        [
            new("Bowl", [new("Karides Bowl", "330₺"), new("Falafel Bowl", "300₺"), new("Tavuk Bowl", "250₺"), new("köfte bowl", "330₺")]),
            new("Aperatifler", [new("Patates kızartması", "150₺")]),
            new("pizzalar", [new("Margarita", "290₺"), new("Karışık Pizza", "340₺"), new("Dört Peynirli Pizza", "380₺"), new("Bolonez Pizza", "340₺"), new("Sebzeli Pizza", "340₺"), new("Barbekü Tavuklu Pizza", "340₺"), new("Nutella Calzone", "280₺"), new("Köz Patlıcanlı Pizza", "340₺"), new("Peperoni Pizza", "380₺"), new("Füme Kaburga Pizza", "380₺"), new("Şarküteri Pizza", "390₺"), new("3 Mantarlı Pizza", "340₺"), new("Dönerli Pizza", "340₺"), new("Sweet Chili pizza", "340₺"), new("Orivhi Özel Pizza", "380₺"), new("Karidesli Pizza", "380₺"), new("Alfredo Pizza", "380₺")]),
            new("Makarnalar", [new("Jambonlu Sosisli Fettucini", "300₺"), new("Sebzeli Tavuklu Soya Soslu Fettucini", "300₺"), new("Tavuklu Mantarlı Fettucini", "300₺"), new("Pesto Soslu Fettucini", "260₺"), new("Domates Soslu Fettucini", "260₺"), new("Bolonez Soslu Fettucini", "300₺")]),
            new("İçecekler", [new("Coca Cola (330ml)", "70₺"), new("Fanta (330ml)", "70₺"), new("Coca Cola Zero (330ml)", "70₺"), new("Sprite (330ml)", "70₺"), new("Fuse Tea (330ml)", "70₺"), new("Su", "15₺"), new("Soda", "30₺"), new("Ayran", "35₺"), new("Coca Cola (1 Litre)", "130₺"), new("Coca Cola Zero  ( 1 Litre)", "130₺"), new("meyveli soda", "35₺")]),
            new("İlave Ürünler", [new("İlave Tavuk", "100₺"), new("Füme Kaburga", "150₺"), new("Jambon", "110₺"), new("Sucuk", "130₺"), new("Köz Patlıcan", "70₺"), new("Pesto", "70₺"), new("Biber", "-"), new("Mantar", "50₺"), new("Mısır", "-"), new("Zeytin", "-"), new("Mozarella", "130₺"), new("Domates Sos", "50₺"), new("Kuru Domates", "40₺"), new("sosis", "80₺"), new("Rokfor", "130₺"), new("Çedar", "130₺"), new("Parmesan", "130₺"), new("İlave Soslar", "50₺")])
        ]),
        new("Pablo Artisan Coffee", "", "08:00-01:00",
        [
            new("Kampanyalar", [new("İçecek(Americano, Çay, Bitki Çayı, Filtre Kahve) + 2 Rulo Börek ya da Soğuk Sandviç", "185₺"), new("Filtre Kahve Yanında Donut veya Berliner", "175₺"), new("Büyük Boy Filtre Kahve Yanında Donut veya Berliner", "190₺"), new("Americano, Ice Americano veya Filtre Kahve (Gülbahçe Şubesine Özel 10:00-15:00 Arası)", "100₺"), new("Çay + Rulo Börek", "100₺")]),
            new("PABLO NEW SPECIALS", [new("Peanuts Latte", "190₺"), new("Apple Pie Latte", "190₺"), new("Pistachio Latte", "210₺"), new("Toffee Nut Latte", "190₺"), new("Matcha Latte", "190₺")]),
            new("TEA & OTHERS", [new("Berry Dream Meyve Çayı", "160₺"), new("Jasmine Green Tea", "160₺"), new("Keep Calm Bitki Çayı", "160₺"), new("Passion Fruit White Tea", "160₺"), new("Pomegranate Hibiscus", "160₺"), new("Wake Up Bitki Çayı", "160₺"), new("Siyah Çay", "75₺"), new("Türk Kahvesi", "110₺"), new("Türk Kahvesi Double", "135₺"), new("Salep", "190₺"), new("Hot Chocolate", "190₺")]),
            new("3RD WAVE", [new("Chemex", "250₺"), new("Syphon", "250₺"), new("V60", "250₺"), new("Cold Brew", "180₺")]),
            new("FRESH", [new("Cool Acai", "215₺"), new("Cool Berry", "215₺"), new("Cool Lime", "215₺"), new("Mango Roibos", "215₺"), new("Limonata", "180₺"), new("Redbell(Redbull)", "220₺"), new("Kiko", "215₺"), new("Holosen", "215₺")])
        ]),
        new("Rektörlük Kantin", "+90-232-750-6329", "10:00-20:00",
        [
            new("Menü", [new("Tavuk Döner + Patates Kızartması + Ayran", "125₺"), new("Mantı", "90₺"), new("Karışık Sandviç + Patates + Ayran", "125₺"), new("Hamburger + Patates Kızartması + Kola", "135₺"), new("Tost + Patates + Ayran", "125₺")])
        ]),
        new("Rodones Coffee", "+90-536-608-3577", "10:00-01:00",
        [
            new("SICAK KAHVELER", [new("Espresso", "80₺"), new("Double Espresso", "90₺"), new("Lungo", "95₺"), new("Americano", "125₺"), new("Filtre Kahve", "125₺"), new("Cortado", "120₺"), new("Coffee Latte", "140₺"), new("Cappucino", "145₺"), new("Flat White", "145₺"), new("Lotus Latte", "165₺"), new("Pumpkin Spice Latte", "165₺"), new("Cookie Caramel Latte", "165₺"), new("Cookie Chocolate Latte", "165₺"), new("Spicy Mango Latte", "165₺"), new("Toffee Nut Latte", "165₺"), new("Vanilya Latte", "165₺"), new("Almond Latte", "165₺"), new("Salted Caramel Latte", "165₺"), new("Mocha", "170₺"), new("White Mocha", "170₺"), new("Caramel Mocha", "170₺"), new("Chai Tea Latte", "175₺")]),
            new("SOĞUK KAHVELER", [new("Ice Americano", "135₺"), new("Ice Latte", "150₺"), new("Ice Flat White", "155₺"), new("Cold Brew", "140₺"), new("Frappe", "170₺"), new("Ice Mocha", "180₺"), new("Ice Caramel Mocha", "180₺")]),
            new("KLASİKLER", [new("Özel Karışım Demleme Çay", "40₺"), new("Özel Karışım Demleme Fincan Çay", "50₺"), new("Take Away Çay", "100₺"), new("Bitki Çayları", "115₺"), new("Türk Kahvesi", "85₺"), new("Sahlep", "155₺"), new("Sıcak Çikolata", "155₺"), new("Sıcak Beyaz Çikolata", "155₺")]),
            new("MOCO COCKTAIL", [new("Cool Lime", "160₺"), new("Berry Hibiscus", "160₺"), new("Lavender Lemonade", "160₺"), new("Karpuz Ve Lychee Lemonade", "160₺"), new("Hypnos", "160₺"), new("Strawberry Lemonade", "160₺"), new("Citrus Punch", "160₺"), new("Ice Tiramis", "160₺"), new("Narsist", "160₺"), new("Mango Mojito", "160₺")]),
            new("FROZEN ÇEŞİTLERİ", [new("Çilek", "160₺"), new("Karadut", "160₺"), new("Mango", "160₺"), new("Karpuz", "160₺"), new("Kavun", "160₺")]),
            new("MILKSHAKE ÇEŞİTLERİ", [new("Vanilya", "165₺"), new("Çikolata", "165₺"), new("Karamel", "165₺"), new("Muz", "165₺"), new("Çilek", "165₺")]),
            new("RODONES'E ÖZEL", [new("Rodones Sıcak", "175₺"), new("Rodones Soğuk", "185₺"), new("Rodones Pistachio", "185₺")]),
            new("3RD WAVE NİTELİKLİ KAHVELER", [new("V60 Demleme", "210₺"), new("Chemex Demleme", "210₺")]),
            new("KASA ÖNÜ ATIŞTIRMALIKLAR", [new("Şans Kurabiyesi", "25₺"), new("Tahıl Bar", "40₺"), new("Bardak Cookie Çeşitleri", "125₺"), new("Glutensiz Bardak Cookie", "135₺")]),
            new("SANDVİÇLER", [new("Beyaz Peynirli Ciabata", "150₺"), new("Hindi Jambonlu Peynirli Bagel", "175₺"), new("Dana Jambonlu Kaşarlı Açma", "155₺"), new("Mozarella Foccacia", "175₺"), new("Ezine Peynirli Ciabata", "180₺"), new("Tavuk Fajita Sandviç", "215₺"), new("Hindi Füme Sandviç", "175₺"), new("Cheesewich", "145₺"), new("Meat And Cheese", "215₺")]),
            new("ÜRÜN İLAVELERİ", [new("Vegan Süt", "30₺"), new("Normal Süt", "30₺"), new("Extra Shot", "30₺"), new("Extra Şurup", "30₺")]),
            new("SOĞUK İÇECEKLER", [new("Su", "30₺"), new("Soda", "55₺"), new("Meyveli Soda", "60₺"), new("Cam Şişe Gazoz", "80₺"), new("Cola, Fanta, Sprite, Ice Tea", "85₺"), new("Churchill", "100₺"), new("Limonata", "130₺"), new("Redbull", "120₺")]),
            new("TATLILAR", [new("Muffin", "70₺"), new("Çikolatalı Cookie", "105₺"), new("Cookie Tart", "130₺"), new("Magnolia Çeşitleri", "140₺"), new("Tiramisu Bardak", "155₺"), new("Belçika Çikolatalı Profiterol", "150₺"), new("San Sebastian", "170₺"), new("Alman Pastası", "170₺"), new("Beyaz Çikolatalı Brownie", "220₺"), new("Panna Cotta", "155₺"), new("Belçika Çikolatalı Cup", "220₺"), new("Antep Fıstıklı Çıtır Cup", "230₺"), new("Coconut Pasta", "230₺")])
        ]),
        new("Ru Coffee House & More", "+90-533-462-0336", "10:00-24:00",
        [
            new("Specials", [new("Coco Monk", "230₺"), new("Cookie Mocha", "230₺"), new("Dirty Chai Latte", "230₺"), new("Fit Peanut", "250₺"), new("Tiramisu Latte", "275₺"), new("Pistachio Cheesecake Latte", "280₺"), new("Tarçınlı Cappuccino", "200₺"), new("Orange Americano", "-")]),
            new("Matcha & Çaylar", [new("Matcha Latte", "180₺"), new("Vanilya Matcha", "200₺"), new("Chai Matcha", "200₺"), new("Coco Matcha", "200₺"), new("Şifacı", "160₺"), new("Papatya&Melisa", "160₺"), new("Okaliptus&Defne", "160₺"), new("Yeşil Çay", "150₺"), new("Kırmızı Meyve Çayı", "160₺")]),
            new("BAKERY", [new("Beyaz Çikolatalı Brownie", "280₺"), new("Glutensiz Brownie", "280₺"), new("Vişneli Brownie", "280₺"), new("Antep Fıstığı ve Frambuazlı Tart", "280₺"), new("Earl Grey Limon Cheesecake", "280₺"), new("Çikolatalı Cheesecake", "280₺"), new("Cinnamon Roll", "200₺"), new("Mozzarella Roll", "150₺"), new("Vişne Çikolata Magnolya", "240₺"), new("Portakal Çikolata Magnolya", "240₺")]),
            new("Kahveler", [new("Double espresso", "140₺"), new("Double Macchiato", "140₺"), new("Türk Kahvesi", "110₺"), new("Americano", "150₺"), new("Filtre Kahve", "150₺"), new("Black Eye", "230₺"), new("Latte", "190₺"), new("Cappuccino", "190₺"), new("Cortado", "180₺"), new("Flat White", "190₺"), new("Mocha(Dark/White)", "220₺"), new("Caramel Macchiato", "220₺"), new("Chai Latte", "200₺"), new("Sıcak Çikolata", "200₺")]),
            new("Demleme ve Ekstralar", [new("V60", "190₺"), new("Chemex", "380₺"), new("Delter Press", "190₺"), new("Shot", "75₺"), new("Bitkisel Süt", "50₺"), new("Süt/Laktozsuz Süt", "15₺"), new("Şurup", "30₺"), new("Bal", "25₺"), new("Dondurma", "75₺")])
        ]),
        new("Soft Lounge", "+90-534-6208-426", "09:00-24:00",
        [
            new("Beyaz Et", [new("Schnitzel", "350₺"), new("Tavuk Wrap", "450₺"), new("Tavuk Taco", "450₺"), new("Somon Bonfile", "450₺"), new("Çıtır Tavuk Kanat", "450₺"), new("Tavuk Fajita", "500₺"), new("Tavuk Çökertme", "500₺"), new("Ispanak Yatağında Bonfile Tavuk", "500₺"), new("Enginar Yatağında Bonfile Tavuk", "550₺")]),
            new("Pizza", [new("Margarita", "300₺"), new("Füme Etli", "500₺"), new("Vejeteryan", "300₺"), new("Pepperoni", "450₺"), new("Soft Lounge Special", "500₺")]),
            new("Kırmızı Et", [new("Ispanak Yatağında Bonfile", "600₺"), new("Enginar Yatağında Bonfile", "650₺"), new("Et Çökertme", "600₺"), new("Siverek Tava", "750₺"), new("Et Fajita", "600₺"), new("Et Wrap", "550₺"), new("Et Taco", "550₺")]),
            new("Burger Menu", [new("Klasik Burger", "400₺"), new("Magic Mushroom Burger", "430₺"), new("Molly Burger", "475₺"), new("Tavuk Burger", "400₺")]),
            new("Makarna", [new("Penne Alfredo", "300₺"), new("Köri Soslu Penne", "300₺"), new("BBQ Soslu Penne", "300₺"), new("Arrabiatta", "300₺"), new("Pesto Soslu", "300₺"), new("Soya Soslu", "300₺"), new("Chicken Cheese", "300₺"), new("Bolonez Soslu", "375₺"), new("Ton Balıklı", "375₺")]),
            new("Bowl", [new("Acai Bowl", "470₺"), new("Muzlu Smoothie Bowl", "430₺"), new("Elmalı Yulaflı Bowl", "430₺"), new("Vejeteryan Bowl", "400₺"), new("Kahvaltı Bowl", "450₺")]),
            new("Salata", [new("Sezar Salata", "400₺"), new("Greek Salata", "370₺"), new("Izgara Tavuklu Salata", "400₺"), new("Izgara Somonlu Salata", "430₺"), new("Ton Balıklı Salata", "400₺"), new("Akdeniz Salatası", "340₺")]),
            new("Sandviç", [new("İzmir Karışık", "350₺"), new("Magic Mushroom", "300₺"), new("Üç Peynirli", "300₺"), new("Soğuk Sandviç", "300₺")]),
            new("Tost", [new("Ayvalık Tostu", "300₺"), new("Kaşarlı Sucuklu", "250₺"), new("Sayas Peynirli", "250₺"), new("Soft Lounge Special", "350₺")]),
            new("Çorbalar", [new("Mercimek Çorbası", "190₺"), new("Domates Çorbası", "190₺"), new("Balık Çorbası", "250₺")]),
            new("Atıştırmalıklar", [new("Çıtır Kokteyl Tabağı", "450₺"), new("Zeytinyağlı Enginar", "350₺"), new("Füme Etli Cheddarlı Patates", "450₺"), new("Enginar Kalbi", "350₺"), new("Güveçte Kaşarlı Mantar", "350₺"), new("Kızarmış Karnıbahar", "350₺"), new("Çıtır Tavuk Parçaları", "450₺"), new("Nachos", "450₺"), new("Soğan Halkası (8 Adet)", "350₺"), new("Sosis Kızartması (8 Adet)", "350₺"), new("Patates Kızartması", "200₺")]),
            new("Sıcak İçecek", [new("Espresso", "100₺"), new("Double Espresso", "120₺"), new("Americano", "130₺"), new("Filtre Kahve", "130₺"), new("Latte", "140₺"), new("Cortado", "140₺"), new("Flat White", "150₺"), new("Karamel Macchiato", "160₺"), new("Chocolate Mocha", "160₺"), new("White Chocolate Mocha", "160₺"), new("Türk Kahvesi", "100₺"), new("Double Türk Kahvesi", "120₺"), new("Bitki Çayı", "100₺"), new("Sıcak Çikolata", "150₺")]),
            new("Soğuk İçecek", [new("Ice Americano", "140₺"), new("Ice Latte", "150₺"), new("Ice Flat White", "160₺"), new("Ice Karamel Macchiato", "170₺"), new("Ice Chocolate Mocha", "170₺"), new("Ice White Chocolate Mocha", "170₺"), new("Ice Toffee Nut Latte", "170₺"), new("Ice Coconat Latte", "170₺"), new("Frappe", "180₺"), new("Limonata", "120₺"), new("Coca Cola-Fanta", "80₺"), new("Sprite-Ice Tea", "80₺"), new("Soda (Meyveli +20)", "40₺")]),
            new("Smoothie", [new("Oreolu Smoothie", "200₺"), new("Çilekli Smoothie", "200₺"), new("Karpuzlu Smoothie", "200₺"), new("Bal-Muz-Kavun Smoothie", "220₺"), new("Orman Meyveli Smoothie", "250₺")]),
            new("Milkshake", [new("Oreolu Milkshake", "200₺"), new("Çilekli Milkshake", "200₺"), new("Muzlu Milkshake", "200₺"), new("Çikolatalı Milkshake", "200₺"), new("Orman Meyveli Milkshake", "250₺")])
        ]),
        new("Solea Coffee And Bakery", "+90-539-277-0350", "09:00-24:00",
        [
            new("Hot Drinks", [new("Espresso", "120₺"), new("Double Espresso", "130₺"), new("Americano", "160₺"), new("Filtre Kahve", "160₺"), new("Coffee Latte", "170₺"), new("Cappucino", "170₺"), new("Cortado", "170₺"), new("Flat White", "180₺"), new("Coffee Mocha Bitter", "210₺"), new("Caramel Macchiato", "210₺"), new("White Chocolate Mocha", "210₺"), new("Pumpkin Spice Latte", "190₺"), new("Salted Caramel Latte", "210₺"), new("Matcha Latte", "220₺"), new("Pistachio Latte", "220₺"), new("Toffee Nut Latte", "190₺"), new("Gingerbread Latte", "190₺"), new("Lotus Latte", "210₺"), new("Salep", "190₺"), new("Sıcak Çikolata", "190₺"), new("Chai Tea Latte", "190₺"), new("Türk Kahvesi", "120₺"), new("Bitki Çayı", "150₺"), new("Tiramisu Latte", "220₺"), new("Çay", "50₺")]),
            new("Iced Coffee", [new("Ice Americano", "170₺"), new("Cold Brew", "190₺"), new("Ice Latte", "190₺"), new("Ice Caramel Macchiato", "220₺"), new("Ice Flat White", "210₺"), new("Iced Matcha Latte", "230₺"), new("Iced Orange Espresso", "230₺"), new("Iced Lemonade Espresso", "230₺"), new("Iced Chai Tea Latte", "210₺"), new("Iced White Mocha", "230₺"), new("Iced Mocha", "230₺"), new("Iced Pistachio Latte", "240₺"), new("Iced Toffee Nut Latte", "220₺"), new("Iced Gingerbread Latte", "220₺"), new("Iced Tiramisu Latte", "240₺"), new("Iced Lotus Latte", "230₺"), new("Iced Lavander Latte", "220₺")]),
            new("Refresha", [new("Natural Limonia", "170₺"), new("Natural Orange", "190₺"), new("Cool Lime", "220₺"), new("Peach Green Tea Lemonade", "230₺"), new("Berry Hibiscus", "230₺"), new("Orange Mango", "220₺"), new("Pomegranate Lemonade", "180₺"), new("Milkshake (Çilek , Çikolata, vanilya, Karamel)", "230₺"), new("Frozen (Çilek , Mango , Ananas , Orman meyvesi )", "220₺"), new("Frappe (Çikolata , Karamel , Vanilya )", "240₺"), new("Java Chip", "250₺")]),
            new("Bubble Tea", [new("Bubble Black (Çilek , Mango , Ananas , Orman meyvesi )", "230₺"), new("Bubble Green Tea (Çilek , Mango , Ananas , Orman meyvesi )", "230₺"), new("Galaxy", "230₺"), new("Blue Sky", "230₺"), new("Bubble Coffee", "230₺"), new("Brown Sugar Milk Tea", "230₺"), new("Taro Milk Tea", "230₺"), new("Dragon Mango", "230₺"), new("Strawberry Acai", "230₺"), new("Lemonade Bubble", "230₺"), new("Chocolate Milk Tea", "230₺"), new("Honeydew", "230₺"), new("Passionfruit Bubble Tea", "230₺")]),
            new("Premium Moctails", [new("Grass", "250₺"), new("Tuxedo", "250₺"), new("Chili Mango", "250₺"), new("White Peach", "250₺")]),
            new("Tatlılar", [new("Éclair (Çikolata, Ruby, Badem, Lotus)", "270₺"), new("Cream Puff (Choux)", "300₺"), new("New York Rolls", "300₺"), new("Cube Croissants", "330₺"), new("Croissants (Tatlı/Dolgulu)", "330₺"), new("Cookie", "210₺"), new("Trio Chocolate Browni", "260₺"), new("Croissant Waffle / Brüksel Waffle", "300₺"), new("Profiterol", "280₺"), new("Rocher Intense", "300₺"), new("Lotus / Frambuazlı Cheesecake", "280₺"), new("San Sebastian", "280₺"), new("Nutella Croissant", "250₺"), new("Cups (Heaven, Paradise, Dubai, Lotus)", "280₺"), new("Danish", "300₺"), new("Solea Sun", "300₺"), new("Apple Line (Elmalı Tart)", "300₺"), new("Fındık Flan", "280₺"), new("Çikolatalı Flan (Dark Flan)", "280₺"), new("Limonlu Tart", "300₺"), new("Orman meyveli / Çikolatalı Pavlova", "280₺"), new("Opeara", "300₺"), new("Coco Crunch", "300₺"), new("Karpatka", "280₺"), new("Kara Orman Meyveli Pasta", "300₺"), new("Tiramisu", "280₺"), new("Suffle", "300₺"), new("Cedric Antep", "280₺"), new("Tuzlu Karamel Tart", "200₺"), new("Çilek Tart", "300₺"), new("Lemon Cheesecake", "280₺")]),
            new("Kahvaltı", [new("Sade Kruvasan / Foccacio", "170₺"), new("Sandviç (Hindi / Mozarella) - (Kruvasan/Chiabatta/Foccacio)", "340₺"), new("Sandviç (Dana Kaburga) - (Kruvasan/Chiabatta/Foccacio)", "350₺"), new("Kuru Domatesli Kiş", "250₺"), new("Ispanaklı Kiş", "250₺"), new("Pesto Bun", "250₺"), new("Akdeniz Danish", "250₺"), new("Peanie Bowl", "250₺"), new("Acai Bowl", "250₺")]),
            new("Grabgo", [new("Lotus Bisküvisi", "8₺"), new("Zencefilli Kurabiye", "70₺"), new("Damla sakızlı kurabiye", "135₺"), new("Yaprak Galeta / Çörekotlu", "130₺"), new("Keçi Boynuzlu Kurabiye", "155₺"), new("Greçka Kurabiye", "160₺"), new("Bardak Biscotti", "170₺")]),
            new("Çekirdek Kahveler", [new("Ruanda , Kenya , Peru , Espresso", "500₺")]),
            new("Extra", [new("İlave Aroma", "40₺"), new("İlave Shot", "40₺"), new("Vegan Süt", "50₺"), new("Belçika çikolatası", "40₺")])
        ]),
        new("Sos Amanos Döner", "+90-543-503-9660", "11:00-01:00",
        [
            new("Menüler", [new("Antakya Menü", "230₺"), new("İskenderun Menü", "240₺"), new("Zurna Antakya Menü", "260₺"), new("Zurna İskenderun Menü", "270₺"), new("Combo Antakya Menü", "260₺"), new("Combo İskenderun Menü", "280₺"), new("Zurna Combo Menü", "300₺"), new("Zurna İsk Combo Menü", "310₺")]),
            new("İskenderun Usulü Dürümler", [new("İskenderun Tavuk Dürüm", "170₺"), new("İskenderun Zurna Dürüm", "210₺"), new("İskenderun Jumbo Dürüm", "210₺")]),
            new("Antakya Usulü Dürümler", [new("Antakya Tavuk Dürüm", "160₺"), new("Antakya Zurna Dürüm", "210₺"), new("Antakya Jumbo Dürüm", "210₺")]),
            new("Extralar", [new("Kaşarlı", "40₺"), new("Psikopat", "15₺"), new("Çift Lavaş", "15₺"), new("K. Patates", "80₺"), new("B. Patates", "140₺")]),
            new("İçecekler", [new("Küçük Ayran", "20₺"), new("Büyük Ayran", "30₺"), new("Kutu Coca Cola", "60₺"), new("Kutu Cola Zero", "60₺"), new("Kutu Fanta", "60₺"), new("Ice Tea Şeftali", "60₺"), new("Ice Tea Limon", "60₺"), new("Şalgam Acılı", "45₺"), new("Şalgam Acısız", "45₺"), new("Şişe Su 500 ml", "15₺"), new("1 Litre Cola", "80₺"), new("2.5 Litre Cola", "110₺"), new("Sade Soda", "25₺"), new("Meyveli Soda", "35₺"), new("Niğde Gazoz", "45₺")])
        ]),
        new("Surrender & Eat", "+90-505-386-3531", "11:00-01:00",
        [
            new("Gel-Al Siparişe Özel", [new("Antakya Döner (M) + Ayran", "150₺")]),
            new("Kampanyalı Menüler", [new("Doyasıya Menü", "1050₺"), new("Cehennem İkilisi", "520₺"), new("Surrender Buluşması", "1700₺"), new("2'li Antakya Menü", "510₺"), new("3'lü Antakya Menü", "765₺"), new("2'li Zurna Menü", "590₺"), new("Öğrenci Menü (SADECE ÖĞRENCİYE ÖZEL )", "250₺")]),
            new("Gurme", [new("Surrender Döner", "180₺"), new("Kaşarlı Döner", "190₺"), new("Cheddarlı Döner", "200₺"), new("İskenderun Döner", "185₺"), new("Soğansever Döner", "185₺"), new("Mexican Döner", "165₺")]),
            new("Dürümler", [new("Antakya Döner", "160₺"), new("Bazuka Döner", "185₺"), new("Zurna Döner", "210₺"), new("Duble Döner", "270₺")]),
            new("Menüler", [new("Surrender Menü", "280₺"), new("Kaşarlı Menü", "280₺"), new("İskenderun Menü", "280₺"), new("Zurna Menü", "300₺"), new("Duble Menü", "360₺"), new("Antakya Menü", "260₺")]),
            new("Servisler", [new("Antakya Servis", "300₺"), new("Pilav Üstü Döner", "340₺"), new("Brost", "320₺"), new("Surrender Special", "240₺"), new("Surrender Crispy (YENİ)", "270₺")]),
            new("İçecekler", [new("Ayran", "35₺"), new("Küçük Ayran", "25₺"), new("Pepsi", "50₺"), new("Pepsi Max", "50₺"), new("Yedigün", "50₺"), new("7 Up", "50₺"), new("Lipton Ice Tea", "50₺"), new("Tropicana", "50₺"), new("Soda", "30₺"), new("Meyveli Soda", "-"), new("ŞALGAM (doğanay gurme)", "50₺"), new("Su", "15₺")])
        ]),
        new("Uğrak Nokta", "+90-554-251-1301", "12:00-02:00",
        [
            new("Enfes Çıtır Tavuk", [new("Tek Pilav + Çıtır Tavuk", "180₺"), new("Duble Pilav + Duble Çıtır Tavuk", "320₺"), new("Çıtır Tavuk + Kajun Patates Tabağı", "250 ₺"), new("Hollanda Patates + Çıtır Tavuk", "300₺"), new("Çıtır Tavuk + Kremalı Tavuklu Makarna", "270₺")]),
            new("Fırsat Menüler", [new("Tavuklu Kremali Makarna + Ayran", "170₺"), new("Köri Soslu Tavuklu Makarna + Ayran", "170₺"), new("Tavuklu Pilav + Ayran", "140₺"), new("Battalbey Çiğ Köfte + Ayran", "80₺"), new("Çıtır Tavuk +Kajun Patates", "250₺")]),
            new("Deli Atıştırmalık", [new("Kajun Patates", "150₺"), new("Hollanda Patates", "180₺")]),
            new("Makarnalar (Şiddetle Tavsiye)", [new("Kremalı Tavuklu Makarna + Ayran", "170₺"), new("Sade Kremalı Makarna + Ayran", "150₺"), new("Köri Soslu Kremalı Tavuklu + Ayran", "170₺"), new("Çıtır Tavuk + Kremalı Tavuklu Makarna", "270₺")]),
            new("Pilav (İddalıyız)", [new("Sade Pilav + Ayran", "120₺"), new("Tek Tavuklu Pilav + Ayran", "140₺"), new("Tek Tavuklu Nohutlu Pilav + Ayran", "150₺"), new("1.5 Porsiyon Tavuklu Pilav + Ayran", "180₺"), new("1.5 Porsiyon Tavuklu Nohutlu Pilav + Ayran", "190₺"), new("Duble Tavuklu Pilav + Ayran", "220₺"), new("Dilerseniz Sote Tavuk İle", "40₺")]),
            new("Ekmek Arası Lezzetler", [new("Ekmek Arası Kasap Sucuk", "170₺"), new("Ekmek Arası Sucuk Kaşar", "180₺"), new("Ekmek Arası Sucuk Kaşar Yumurta", "200₺"), new("Ekmek Arası Sosisli Anne Patso", "175₺"), new("Karışık Sandviç", "175₺"), new("Ekmek Arası Köfte", "175₺"), new("Balık Ekmek", "150₺")]),
            new("Sporcu Menüler", [new("250 gr Pilav-250 gr Tavuk", "300₺"), new("500 gr Pirzola - 250 gr Pilav", "350₺"), new("200 gr Pilav-200 gr Köfte", "350₺")]),
            new("Battalbey Çiğköfte", [new("Dürüm Çiğköfte +Ayran", "80₺"), new("Duble Dürüm Çiğköfte +Ayran", "120₺"), new("Patatesli Çiğköfte (Patci)", "120₺"), new("Doritoslu Çiğköfte (Çift Lavaş)", "100₺"), new("Porsiyon 250 gr", "175₺"), new("Porsiyon 500 gr", "350 ₺")]),
            new("Izgara Kg Menüler", [new("Tavuk Pirzola", "350₺"), new("Sucuk", "400₺"), new("Köfte", "500₺")]),
            new("Tatlı Krizi", [new("Tiramisu", "180₺"), new("Limonlu Cheesecake", "180₺"), new("Nutellalı Pasta", "200₺")]),
            new("İçecekler", [new("Coca Cola", "75₺"), new("Fanta", "75₺"), new("Sprite", "75₺"), new("Ayran (Küçük)", "20₺"), new("Ayran (Büyük)", "40₺"), new("Şalgam", "45₺"), new("Su", "20₺"), new("Soda", "30₺"), new("1 Lt Coca Cola", "100₺")])
        ]),
        new("Uğur Cafe", "+90-536-833-3488", "10:00-23:00",
        [
            new("Menüler", [new("Tavuk Menü Patates + Kola", "260₺"), new("Tavuk Menü Patates + Ayran", "250₺"), new("Karışık Menü Patates + Kola", "300₺"), new("Karışık Menü Patates + Ayran", "290₺"), new("Köfte Menü Patates + Kola", "260₺"), new("Köfte Menü Patates + Ayran", "250₺"), new("Patso Menü Patates + Kola", "300₺"), new("Patso Menü Patates + Ayran", "290₺"), new("Ayvalık Menü Patates + Kola", "280₺"), new("Ayvalık Menü Patates + Ayran", "270₺")]),
            new("Sandviçler", [new("Karışık Sandviç", "200₺"), new("Köfte Ekmek", "150₺"), new("Tavuk Döner Ekmek", "150₺"), new("Tavuk Döner Dürüm", "150₺"), new("Patso", "200₺"), new("Ayvalık Tostu", "180₺"), new("Sucuk Ekmek", "150₺"), new("Sucuklu Yumurta", "160₺"), new("Patates Kaşar Tavuk Dürüm", "180₺"), new("Balık Ekmek", "160₺")]),
            new("Izgaralar", [new("Izgara Köfte", "300₺"), new("Porsiyon Tavuk İncik", "300₺"), new("Sardalye Tava", "300₺")]),
            new("Makarnalar", [new("Kremalı Tavuklu", "250₺"), new("Kremalı Mantarlı Tavuklu", "250₺"), new("Köri Soslu Tavuklu", "250₺"), new("Soya Soslu Tavuklu", "250₺"), new("Efsane Cafe Special", "250₺"), new("Penne Arabiata Acılı", "250₺"), new("Carbonara", "250₺")]),
            new("Çiğ Köfteler", [new("Battal bey Çiğ Köfte", "100₺"), new("Patatesli Çiğ köfte", "150₺"), new("Doritoslu Çiğ Köfte", "120₺")]),
            new("Burgerler", [new("Hamburger", "200₺"), new("Cheeseburger", "220₺"), new("Chickenburger", "200₺")]),
            new("Aperatifler", [new("Patates Kızartması", "170₺"), new("Elma Dilim Patates", "170₺")]),
            new("Waffle", [new("İdeal Waffle", "300₺")]),
            new("İçecekler", [new("Coca-Cola, Sprite, Fanta, Ice-Tea Çeşitleri", "70₺"), new("Ayran Küçük", "30₺"), new("Ayran Büyük", "40₺"), new("1 Litre Coca Cola", "100₺"), new("2 Litre Coca Cola", "150₺"), new("Su", "20₺"), new("Soda", "30₺"), new("Çay", "30₺"), new("Nescafe", "70₺"), new("Türk Kahvesi", "70₺")])
        ]),
        new("Yıldız Gözleme", "+90-536-620-0250", "07:00-20:00",
        [
            new("Gözleme", [new("Kıymalı", "165₺"), new("Karışık", "170₺"), new("Patatesli", "150₺"), new("Otlu - Lorlu", "150₺"), new("Patlıcanlı", "150₺"), new("Lorlu", "150₺"), new("Patates Kaşar", "155₺"), new("Patlıcan Kaşar", "155₺"), new("Ot Kaşar", "155₺"), new("Tahinli", "220₺"), new("kıymalı kaşarlı", "170₺"), new("domates kaşar", "170₺"), new("kıymalı yumurtalı", "180₺"), new("yumurtalı kaşarlı", "180₺"), new("yumurtalı", "150₺"), new("sucuklu kaşarlı", "170₺"), new("salam kaşar", "170₺"), new("Ciğerli", "150₺"), new("Tavuklu", "150₺"), new("Soğanlı Kaşarlı", "160₺"), new("Soğanlı Yumurtalı", "160₺"), new("Tavuklu Kaşarlı", "160₺"), new("Ciğerli Kaşarlı", "160₺"), new("Kaşarlı", "160₺"), new("Beyaz Peynirli", "165₺"), new("3 Peynirli", "180₺"), new("Nutellalı", "220₺"), new("mantar kaşar", "175₺"), new("3lü", "180₺")]),
            new("Aparatif", [new("arnavut böğreği (Otlu)", "135₺"), new("sigara böğreği", "120₺"), new("Pişi Tabağı", "150₺"), new("turşulu pişi", "60₺"), new("peynirli pişi", "50₺"), new("sıkma (lorlu)", "80₺"), new("sıkma(kaşarlı)", "100₺"), new("arnavut böğreği kıymalı", "150₺"), new("kaşarlı pişi", "60₺")]),
            new("İçecekler", [new("Ayran", "30₺"), new("Ayran (Büyük)", "40₺"), new("Açık Ayran", "60₺"), new("Cola (Cam Şişe)", "60₺"), new("Fanta (Cam Şişe)", "60₺"), new("Gazoz (Cam Şişe)", "60₺"), new("Ufak Meyve Suyu", "30₺"), new("Çay", "20₺"), new("Kahve", "60₺"), new("su", "30₺"), new("cola(1litre)", "110₺"), new("ıce-tea", "60₺"), new("ayran(1litre)", "95₺")])
        ]),
        new("Zeybek Kokoreç", "+90-531-914-7970", "10:00-01:00",
        [
            new("Yiyecekler", [new("Kokoreç", "250₺"), new("Sucuk Ekmek", "180₺"), new("Köfte Ekmek", "250₺"), new("Tavuk Ekmek", "180₺"), new("Çiğ Köfte", "-"), new("Adana Dürüm", "250₺"), new("Tavuk Dürüm", "180₺"), new("Urfa Dürüm", "-"), new("Tavuk Pilav", "180₺"), new("Çeyrek Kokoreç", "150₺")]),
            new("İçecekler", [new("Küçük Ayran", "25₺"), new("Tombul Ayran", "40₺"), new("Şalgam", "50₺"), new("Kola", "60₺"), new("Fanta", "60₺"), new("Niğde Gazoz", "60₺"), new("Soda", "25₺"), new("Su", "20₺")])
        ])
    ];

    private static string ToSlug(string name)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var c in name.ToLowerInvariant())
        {
            switch (c)
            {
                case '\u011f': sb.Append('g'); break; // ğ
                case '\xfc': sb.Append('u'); break;   // ü
                case '\u015f': sb.Append('s'); break; // ş
                case '\u0131': sb.Append('i'); break; // ı
                case '\xf6': sb.Append('o'); break;   // ö
                case '\xe7': sb.Append('c'); break;   // ç
                case ' ': sb.Append('-'); break;
                case '&': sb.Append('-'); break;
                default:
                    if (char.IsLetterOrDigit(c)) sb.Append(c);
                    break;
            }
        }
        var slug = sb.ToString().Trim('-');
        while (slug.Contains("--")) slug = slug.Replace("--", "-");
        return slug;
    }

    private static decimal ParsePrice(string price)
    {
        if (string.IsNullOrWhiteSpace(price) || price.Trim() == "-") return 0m;
        var digits = new string(price.Where(char.IsDigit).ToArray());
        return decimal.TryParse(digits, out var result) ? result : 0m;
    }

    private static (TimeOnly Open, TimeOnly Close) ParseHours(string saat)
    {
        if (string.IsNullOrEmpty(saat)) return (new TimeOnly(9, 0), new TimeOnly(22, 0));
        var parts = saat.Split('-');
        if (parts.Length != 2) return (new TimeOnly(9, 0), new TimeOnly(22, 0));
        return (ParseTime(parts[0]), ParseTime(parts[1]));
    }

    private static TimeOnly ParseTime(string t)
    {
        var p = t.Trim().Split(':');
        return p.Length == 2 && int.TryParse(p[0], out var h) && int.TryParse(p[1], out var m)
            ? new TimeOnly(h % 24, m)
            : new TimeOnly(9, 0);
    }

    private static string Trunc(string s, int max) => s.Length > max ? s[..max] : s;

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied.");

            await SeedRolesAsync(roleManager, logger);
            await SeedUsersAsync(userManager, logger);
            await SeedRestaurantsAsync(context, userManager, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        foreach (var role in AppRoles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Created role: {Role}", role);
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger logger)
    {
        if (await userManager.FindByEmailAsync("admin@arrivo.io") is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@arrivo.io",
                Email = "admin@arrivo.io",
                EmailConfirmed = true,
                FirstName = "Platform",
                LastName = "Admin",
                PhoneNumber = "+905000000000"
            };
            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.PlatformAdmin);
                logger.LogInformation("Seeded platform admin user.");
            }
        }

        if (await userManager.FindByEmailAsync("kasiyer@arrivo.io") is null)
        {
            var cashier = new ApplicationUser
            {
                UserName = "kasiyer@arrivo.io",
                Email = "kasiyer@arrivo.io",
                EmailConfirmed = true,
                FirstName = "Demo",
                LastName = "Kasiyer",
                PhoneNumber = "+905009876543"
            };
            var result = await userManager.CreateAsync(cashier, "Cashier123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(cashier, AppRoles.Cashier);
                logger.LogInformation("Seeded cashier user.");
            }
        }
    }

    private static async Task SeedRestaurantsAsync(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger logger)
    {
        if (await context.Restaurants.AnyAsync())
            return;

        foreach (var def in AllRestaurants)
        {
            var slug = ToSlug(def.Name);
            var email = $"{slug}@arrivo.io";

            var owner = await userManager.FindByEmailAsync(email);
            if (owner is null)
            {
                var phone = string.IsNullOrEmpty(def.Phone) ? "+90000000000" : def.Phone;
                owner = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = Trunc(def.Name, 50),
                    LastName = "Sahibi",
                    PhoneNumber = Trunc(phone, 20)
                };
                var result = await userManager.CreateAsync(owner, "Owner123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(owner, AppRoles.RestaurantOwner);
                }
                else
                {
                    logger.LogWarning("Could not create owner for {Name}: {Errors}",
                        def.Name, string.Join(", ", result.Errors.Select(e => e.Description)));
                    continue;
                }
            }

            var (open, close) = ParseHours(def.Saat);
            var restaurantPhone = string.IsNullOrEmpty(def.Phone) ? "+90000000000" : Trunc(def.Phone, 20);

            var restaurant = Restaurant.Create(
                name: Trunc(def.Name, 100),
                description: string.Empty,
                phoneNumber: restaurantPhone,
                address: "Gülbahçe Köyü, Urla/İzmir",
                openingTime: open,
                closingTime: close,
                minimumOrderAmount: 50m,
                ownerId: owner.Id
            );
            restaurant.Approve();

            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded restaurant: {Name} (Id={Id})", def.Name, restaurant.Id);

            var items = new List<MenuItem>();
            foreach (var cat in def.Cats)
            {
                var catName = Trunc(cat.Name, 60);
                foreach (var item in cat.Items)
                {
                    var price = ParsePrice(item.Price);
                    items.Add(MenuItem.Create(
                        name: Trunc(item.Name, 100),
                        description: string.Empty,
                        price: price,
                        category: catName,
                        restaurantId: restaurant.Id
                    ));
                }
            }

            context.MenuItems.AddRange(items);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} menu items for {Name}.", items.Count, def.Name);
        }
    }
}