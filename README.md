# 🛵 Arrivo

> **Arrivo** — İtalyanca'da *varış* anlamına gelir.
> Gülbahçe Köyü & İYTE Kampüsü'ne özel çok restoranlı yemek sipariş platformu.
> Bölgede Yemeksepeti boşluğunu kapatan ilk ve tek platform. **arrivo.com.tr**

---

## 🧩 Neden Arrivo?

Gülbahçe Köyü ve İzmir Yüksek Teknoloji Enstitüsü (İYTE) kampüsünde yaşayan binlerce kişi, ülke genelinde yaygın yemek platformlarının kapsama alanı dışında kalıyordu. Arrivo, bu boşluğu doldurmak için tasarlandı: bölgeye özgü restoranları, gerçek zamanlı sipariş takibi ile tek çatı altında birleştiren modern bir teslimat platformu.

---

## 📦 Tech Stack

| Katman | Teknoloji |
|---|---|
| Backend API | ASP.NET Core 8 Web API (Clean Architecture) |
| Veritabanı | PostgreSQL + EF Core |
| Auth | JWT + ASP.NET Identity |
| Gerçek Zamanlı | SignalR (OrderHub) |
| Fiş Yazıcı | ESC/POS Thermal (Türkiye KDV %8) |
| Web Frontend | React + Vite + TypeScript |
| Mobile (Faz 3) | React Native |

---

## 🗂 Proje Yapısı

```
arrivo/
├── src/
│   ├── Arrivo.API/              # ASP.NET Core Web API — controller'lar, middleware, DI
│   ├── Arrivo.Application/      # CQRS, Use Cases, DTOs, interface'ler
│   ├── Arrivo.Domain/           # Entity'ler, Enum'lar, Domain Mantığı
│   └── Arrivo.Infrastructure/   # DB, Identity, SignalR, Servisler
├── frontend/                    # React + Vite (Müşteri + Admin + POS)
├── docs/                        # API dökümantasyonu
└── .github/workflows/           # CI/CD
```

---

## 🚀 Yol Haritası

| Faz | Açıklama | Durum |
|-----|----------|-------|
| **Faz 1 — MVP** | Fabrika Kitchen ile tek restoran | 🔨 Aktif |
| **Faz 2 — Platform** | Çok restoran, başvuru/onay sistemi | 📋 Planlı |
| **Faz 3 — Mobile** | React Native iOS & Android | 📋 Planlı |
| **Faz 4 — Büyüme** | Komisyon, kampanya, kurye takibi | 📋 Planlı |

---

## ⚡ Kurulum

### Backend

```bash
cd src/Arrivo.API
cp appsettings.example.json appsettings.Development.json
# appsettings.Development.json dosyasını düzenle (DB bağlantısı, JWT key, vb.)
dotnet ef database update
dotnet run
```

### Frontend

```bash
cd frontend
cp .env.example .env
npm install
npm run dev
```

---

## 👥 Kullanıcı Rolleri

| Rol | Açıklama |
|-----|----------|
| `customer` | Sipariş veren son kullanıcı |
| `cashier` | POS kasa ekranı kullanıcısı |
| `restaurant_owner` | Kendi restoran panelini yöneten işletmeci |
| `platform_admin` | Tüm platformu gören süper admin |

---

## 🌐 Domain

**arrivo.com.tr** — Gülbahçe Köyü & İYTE Kampüsü'ne özel teslimat platformu.

---

*Arrivo — MVP v1.0 · arrivo.com.tr*
