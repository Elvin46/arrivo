# 🛵 Gülbahçe Yemek Platformu

> Gülbahçe Köyü & İYTE Kampüsü'ne özel çok restoranlı yemek sipariş platformu.  
> Bölgede Yemeksepeti boşluğunu kapatan ilk ve tek platform.

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
gulbahcesepeti/
├── src/
│   ├── GulbahceSepeti.API/          # ASP.NET Core Web API
│   ├── GulbahceSepeti.Application/  # CQRS, Use Cases, DTOs
│   ├── GulbahceSepeti.Domain/       # Entities, Enums, Domain Logic
│   └── GulbahceSepeti.Infrastructure/ # DB, Identity, SignalR, Services
├── frontend/                        # React + Vite (Müşteri + Admin + POS)
├── docs/                            # API dökümantasyonu
└── .github/workflows/               # CI/CD
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
cd src/GulbahceSepeti.API
cp appsettings.example.json appsettings.Development.json
# appsettings.Development.json'ı düzenle
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

*Gülbahçe Yemek Platformu — MVP v1.0*
