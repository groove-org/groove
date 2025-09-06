import {
  CalendarIcon,
  CreditCardIcon,
  MagnifyingGlassIcon,
  MusicalNoteIcon,
  ChatBubbleLeftRightIcon,
} from "@heroicons/react/24/outline";

const features = [
  {
    title: "Powerful Search",
    desc: "Filter by location, fee range, genre, availability, and ratings.",
    icon: MagnifyingGlassIcon,
  },
  {
    title: "Artist Profiles",
    desc: "Reels, tech riders, socials, past gigs — all verified in one sheet.",
    icon: MusicalNoteIcon,
  },
  {
    title: "Smart Requests",
    desc: "Send briefs, collect applications, compare offers across artists.",
    icon: CalendarIcon,
  },
  {
    title: "Contracts & Payments",
    desc: "Click-to-accept contracts, deposits, and payouts with protection.",
    icon: CreditCardIcon,
  },
  {
    title: "Inbox & Chat",
    desc: "Centralize communication with message templates and file sharing.",
    icon: ChatBubbleLeftRightIcon,
  },
];

export default function FeatureCards() {
  return (
    <section id="features" className="bg-slate-950">
      <div className="mx-auto max-w-6xl px-4 py-16 md:px-6 md:py-24">
        <h2 className="text-3xl font-bold tracking-tight text-white md:text-4xl">
          Everything to run smooth bookings
        </h2>
        <p className="mt-3 max-w-2xl text-slate-300">
          Groove replaces spreadsheets and DMs with a single, reliable workflow.
        </p>

        <div className="mt-10 grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
          {features.map(({ title, desc, icon: Icon }) => (
            <div
              key={title}
              className="group relative overflow-hidden rounded-2xl border border-white/10 bg-slate-900 p-6"
            >
              <div className="mb-4 inline-grid h-10 w-10 place-items-center rounded-xl bg-fuchsia-600/20">
                <Icon className="h-6 w-6 text-fuchsia-400" />
              </div>
              <h3 className="text-lg font-semibold text-white">{title}</h3>
              <p className="mt-2 text-sm leading-relaxed text-slate-300">
                {desc}
              </p>
              <div className="pointer-events-none absolute -right-10 -top-10 h-24 w-24 rounded-full bg-fuchsia-500/10 blur-2xl transition-opacity group-hover:opacity-100" />
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
