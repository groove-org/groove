"use client";

import { useState } from "react";
import Link from "next/link";
import { Bars3Icon, XMarkIcon } from "@heroicons/react/24/outline";
import clsx from "clsx";

const nav = [
  { name: "Product", href: "#product" },
  { name: "Features", href: "#features" },
  { name: "Pricing", href: "#pricing" },
  { name: "Contact", href: "#contact" },
];

export default function Navbar() {
  const [open, setOpen] = useState(false);

  return (
    <header className="sticky top-0 z-50 border-b border-white/10 bg-slate-900/70 backdrop-blur">
      <nav className="mx-auto flex max-w-6xl items-center justify-between px-4 py-4 md:px-6">
        {/* Logo */}
        <Link href="/" className="group inline-flex items-center gap-2">
          <span className="inline-grid h-8 w-8 place-items-center rounded-full bg-fuchsia-600 text-white font-bold">
            gr
          </span>
          <span className="text-lg font-semibold tracking-tight text-white group-hover:text-fuchsia-300">
            groove
          </span>
        </Link>

        {/* Desktop nav */}
        <ul className="hidden items-center gap-8 md:flex">
          {nav.map((n) => (
            <li key={n.name}>
              <a
                href={n.href}
                className="text-sm text-slate-200 hover:text-white"
              >
                {n.name}
              </a>
            </li>
          ))}
        </ul>

        {/* Actions */}
        <div className="hidden items-center gap-3 md:flex">
          <Link
            href="/login"
            className="rounded-full border border-white/20 px-4 py-2 text-sm text-slate-100 hover:border-fuchsia-400 hover:text-white"
          >
            Login
          </Link>
          <Link
            href="/signup"
            className="rounded-full bg-fuchsia-600 px-4 py-2 text-sm font-semibold text-white hover:bg-fuchsia-500"
          >
            Sign up
          </Link>
        </div>

        {/* Mobile button */}
        <button
          onClick={() => setOpen((v) => !v)}
          className="inline-flex items-center justify-center rounded-md p-2 text-slate-200 md:hidden"
          aria-label="Toggle navigation"
        >
          {open ? (
            <XMarkIcon className="h-6 w-6" />
          ) : (
            <Bars3Icon className="h-6 w-6" />
          )}
        </button>
      </nav>

      {/* Mobile panel */}
      <div className={clsx("md:hidden", open ? "block" : "hidden")}>
        <div className="space-y-1 border-t border-white/10 px-4 pb-4 pt-2">
          {nav.map((n) => (
            <a
              key={n.name}
              href={n.href}
              onClick={() => setOpen(false)}
              className="block rounded-lg px-3 py-2 text-slate-200 hover:bg-white/5"
            >
              {n.name}
            </a>
          ))}
          <div className="mt-2 flex gap-2">
            <Link
              href="/login"
              className="flex-1 rounded-lg border border-white/10 px-3 py-2 text-center text-slate-200 hover:bg-white/5"
            >
              Login
            </Link>
            <Link
              href="/signup"
              className="flex-1 rounded-lg bg-fuchsia-600 px-3 py-2 text-center font-semibold text-white hover:bg-fuchsia-500"
            >
              Sign up
            </Link>
          </div>
        </div>
      </div>
    </header>
  );
}
