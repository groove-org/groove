// next.config.ts
import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "www.dj-lab.de",
      },
      {
        protocol: "https",
        hostname: "images.unsplash.com", // Unsplash
      },
    ],
  },
};

export default nextConfig;
