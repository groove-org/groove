"use client";

import Image from "next/image";
import Link from "next/link";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import {
  Calendar,
  Music2,
  MapPin,
  ShieldCheck,
  Star,
  Sparkles,
} from "lucide-react";

export default function Home() {
  return (
    <div className="min-h-screen bg-gradient-to-b from-black via-[#0a0a0a] to-black text-white">
      <SiteNav />
      <Hero />
      <main className="mx-auto w-full max-w-7xl px-4 md:px-6">
        <SectionIntro
          eyebrow="Warum Groove?"
          title="Artists buchen & Events planen – alles in einem Flow"
          subtitle="Transparente Profiles, einfache Anfragen und Tools für den Event‑Alltag."
        />
        <ValueGrid />

        <Separator className="my-12 bg-white/10" />

        <SectionIntro
          eyebrow="So funktioniert’s"
          title="Von der Idee bis zum Gig"
          subtitle="In drei Schritten zum Booking oder zur fertigen Event‑Seite."
        />
        <HowItWorks />

        <Separator className="my-12 bg-white/10" />

        <PreviewStrip />

        <CTA />
      </main>
      <Footer />
    </div>
  );
}

function SiteNav() {
  return (
    <div className="sticky top-0 z-40 w-full border-b border-white/10 bg-black/60 backdrop-blur-md">
      <div className="mx-auto flex max-w-7xl items-center justify-between px-4 py-3 md:px-6">
        <Link href="/" className="flex items-center gap-2">
          <div className="h-6 w-6 rounded-md bg-gradient-to-br from-lime-300 to-emerald-400" />
          <span className="text-lg font-semibold tracking-tight">Groove</span>
        </Link>
        <nav className="hidden items-center gap-6 md:flex">
          <a
            className="text-sm text-white/80 hover:text-white"
            href="#features"
          >
            Features
          </a>
          <a className="text-sm text-white/80 hover:text-white" href="#how">
            Ablauf
          </a>
          <a className="text-sm text-white/80 hover:text-white" href="#preview">
            Preview
          </a>
        </nav>
        <div className="flex items-center gap-2">
          <Button asChild variant="secondary" className="hidden md:inline-flex">
            <Link href="#organizer">Ich organisiere Events</Link>
          </Button>
          <Button asChild>
            <Link href="#artist">Ich bin Artist</Link>
          </Button>
        </div>
      </div>
    </div>
  );
}

function Hero() {
  return (
    <section className="relative overflow-hidden">
      {/* Background glow */}
      <div className="pointer-events-none absolute inset-0 -z-10">
        <div className="absolute left-1/2 top-[-15%] h-[52rem] w-[52rem] -translate-x-1/2 rounded-full bg-emerald-500/20 blur-3xl" />
        <div className="absolute right-[-10%] bottom-[-20%] h-[38rem] w-[38rem] rounded-full bg-lime-400/10 blur-3xl" />
      </div>

      <div className="mx-auto grid max-w-7xl grid-cols-1 items-center gap-10 px-4 py-20 md:grid-cols-2 md:px-6 md:py-28">
        <div>
          <Badge className="mb-3 bg-emerald-500/20 text-emerald-200">
            <Sparkles className="mr-1 h-3.5 w-3.5" /> Beta • Booking für Musik &
            Clubs
          </Badge>
          <h1 className="text-balance text-4xl font-semibold tracking-tight md:text-6xl">
            Finde den passenden <span className="text-emerald-300">Act</span>.
            <br /> Plane das <span className="text-lime-300">Event</span>.
          </h1>
          <p className="mt-4 max-w-xl text-pretty text-white/80">
            Groove verbindet Veranstalter:innen, Venues und Artists. Klare
            Profile, direkte Anfragen, (später) Verträge & Zahlung – alles an
            einem Ort.
          </p>
          <div className="mt-6 flex flex-wrap gap-3">
            <Button asChild>
              <Link href="#artist">Kostenloses Artist‑Profil</Link>
            </Button>
            <Button asChild variant="secondary">
              <Link href="#organizer">Event einstellen</Link>
            </Button>
          </div>
          <p className="mt-3 text-xs text-white/60">
            Kostenlos starten • Keine Kreditkarte nötig
          </p>
        </div>

        <div className="relative">
          <div className="relative aspect-[4/3] w-full overflow-hidden rounded-2xl border border-white/10 bg-white/5 shadow-2xl">
            <Image
              src="https://www.dj-lab.de/app/uploads/2022/01/move-d-source-records-nach-16-jahren-wieder-aufleben-1430x845.jpeg"
              alt="Open Air Crowd"
              fill
              className="object-cover"
            />
            <div className="absolute inset-x-0 bottom-0 bg-gradient-to-t from-black/70 to-transparent p-4">
              <div className="flex items-center gap-2 text-sm text-white/80">
                <Calendar className="h-4 w-4" />
                <span>Hamburg • Sa, 20. Sep 2025</span>
                <span>•</span>
                <MapPin className="h-4 w-4" />
                <span>Parkdeck St. Pauli</span>
              </div>
              <div className="mt-1 text-lg font-semibold">
                Modulairé Open Air
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

function SectionIntro({
  eyebrow,
  title,
  subtitle,
}: {
  eyebrow: string;
  title: string;
  subtitle?: string;
}) {
  return (
    <div className="mx-auto mb-6 max-w-3xl text-center">
      <p className="mb-2 text-sm uppercase tracking-widest text-white/60">
        {eyebrow}
      </p>
      <h2 className="text-balance text-3xl font-semibold tracking-tight md:text-4xl">
        {title}
      </h2>
      {subtitle && <p className="mt-3 text-white/70">{subtitle}</p>}
    </div>
  );
}

function ValueGrid() {
  return (
    <section id="features" className="grid grid-cols-1 gap-4 md:grid-cols-3">
      {[
        {
          icon: <Music2 className="h-5 w-5" />,
          title: "Verifizierte Profiles",
          text: "Demos, Tech‑Rider, Referenzen & Kalender – alles transparent an einem Ort.",
        },
        {
          icon: <ShieldCheck className="h-5 w-5" />,
          title: "Sichere Buchung",
          text: "Optionale Verträge, Zahlungs‑Escrow (später) und klare Kommunikation.",
        },
        {
          icon: <Star className="h-5 w-5" />,
          title: "Kuratiertes Matching",
          text: "Finde passende Acts nach Stadt, Genre, Budget & Verfügbarkeit.",
        },
      ].map((f, i) => (
        <Card key={i} className="border-white/10 bg-white/5">
          <CardHeader>
            <CardTitle className="flex items-center gap-2 text-base">
              <span className="inline-flex h-8 w-8 items-center justify-center rounded-full bg-emerald-500/15 text-emerald-300">
                {f.icon}
              </span>
              {f.title}
            </CardTitle>
          </CardHeader>
          <CardContent className="text-sm text-white/75">{f.text}</CardContent>
        </Card>
      ))}
    </section>
  );
}

function HowItWorks() {
  return (
    <section id="how" className="mx-auto mt-4 max-w-5xl">
      <div className="grid grid-cols-1 gap-4 md:grid-cols-3">
        {[
          {
            step: 1,
            title: "Profil erstellen",
            text: "Artists & Venues hinterlegen Musik, Tech‑Rider, Verfügbarkeit & Preise.",
          },
          {
            step: 2,
            title: "Anfragen & bestätigen",
            text: "Direkte Booking‑Anfragen, Angebotsbestätigung und (später) digitale Verträge.",
          },
          {
            step: 3,
            title: "Event managen",
            text: "Timeline, Slots, Kommunikation & (später) Zahlung an einem Ort.",
          },
        ].map((s) => (
          <Card key={s.step} className="border-white/10 bg-white/5">
            <CardHeader>
              <CardTitle className="flex items-center gap-3">
                <span className="inline-flex h-8 w-8 items-center justify-center rounded-full bg-lime-400/20 text-lime-300">
                  {s.step}
                </span>
                <span className="text-base">{s.title}</span>
              </CardTitle>
            </CardHeader>
            <CardContent className="text-sm text-white/75">
              {s.text}
            </CardContent>
          </Card>
        ))}
      </div>
    </section>
  );
}

function PreviewStrip() {
  return (
    <section id="preview" className="mt-2">
      <div className="grid grid-cols-1 gap-4 md:grid-cols-3">
        {[1, 2, 3].map((i) => (
          <Card key={i} className="overflow-hidden border-white/10 bg-white/5">
            <CardContent className="p-0">
              <div className="relative aspect-[16/10]">
                <Image
                  src={
                    i === 1
                      ? "https://images.unsplash.com/photo-1519671482749-fd09be7ccebf?q=80&w=1600&auto=format&fit=crop"
                      : i === 2
                      ? "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?q=80&w=1600&auto=format&fit=crop"
                      : "https://images.unsplash.com/photo-1506157786151-b8491531f063?q=80&w=1600&auto=format&fit=crop"
                  }
                  alt="Preview"
                  fill
                  className="object-cover"
                />
              </div>
              <div className="p-4">
                <div className="flex items-center gap-2 text-sm text-white/70">
                  <Calendar className="h-4 w-4" /> Sa, 20. Sep 2025
                  <span>•</span>
                  <MapPin className="h-4 w-4" /> Hamburg
                </div>
                <div className="mt-1 text-lg font-semibold">
                  Event‑Seite (Preview)
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </section>
  );
}

function CTA() {
  return (
    <section className="mx-auto my-16 max-w-5xl overflow-hidden rounded-2xl border border-white/10 bg-gradient-to-br from-emerald-500/15 to-lime-400/10 p-6 md:p-10">
      <div className="grid grid-cols-1 items-center gap-8 md:grid-cols-2">
        <div>
          <h3 className="text-2xl font-semibold tracking-tight md:text-3xl">
            Starte heute mit Groove
          </h3>
          <p className="mt-2 text-white/80">
            Erstelle ein kostenloses Profil und erhalte die ersten
            Booking‑Anfragen innerhalb von Minuten.
          </p>
          <div className="mt-5 flex flex-wrap gap-3">
            <Button asChild>
              <Link href="#artist">Ich bin Artist</Link>
            </Button>
            <Button asChild variant="secondary">
              <Link href="#organizer">Ich organisiere Events</Link>
            </Button>
          </div>
        </div>
        <div className="relative">
          <div className="relative aspect-[4/3] w-full overflow-hidden rounded-xl border border-white/10 bg-white/5">
            <Image
              src="https://images.unsplash.com/photo-1506157786151-b8491531f063?q=80&w=1600&auto=format&fit=crop"
              alt="DJ deck close‑up"
              fill
              className="object-cover"
            />
          </div>
        </div>
      </div>
    </section>
  );
}

function Footer() {
  return (
    <footer className="mt-8 border-t border-white/10 bg-black/60">
      <div className="mx-auto flex max-w-7xl flex-col items-center justify-between gap-4 px-4 py-8 text-sm text-white/60 md:flex-row md:px-6">
        <div className="flex items-center gap-2">
          <div className="h-5 w-5 rounded-md bg-gradient-to-br from-lime-300 to-emerald-400" />
          <span>© {new Date().getFullYear()} Groove</span>
        </div>
        <div className="flex items-center gap-6">
          <Link href="#">Impressum</Link>
          <Link href="#">Datenschutz</Link>
          <Link href="#">Kontakt</Link>
        </div>
      </div>
    </footer>
  );
}
