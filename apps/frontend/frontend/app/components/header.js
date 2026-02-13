"use client";

import Link from "next/link";
import Image from "next/image";
import { usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import styles from "../styles/header.module.css";

const Header = () => {
  const pathname = usePathname();
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const valid = token && token !== "undefined" && token.trim() !== "";
    setIsAuthenticated(valid);
  }, []);

  const allLinks = [
    { href: "/", src: "/socialNetwork.svg", alt: "SocialNetwork" },
    { href: "/wardrobe", src: "/clothing.svg", alt: "Clothing" },
    { href: "/outfit", src: "/wardrobe.svg", alt: "Wardrobe" },
    { href: "/saved", src: "/bookmarks.svg", alt: "Saved" },
  ];
  
  const visibleLinks = isAuthenticated ? allLinks : [allLinks[0]];

  return (
      <nav className={styles.sidebar}>
        {visibleLinks.map(({ href, src, alt }) => (
            <div
                key={href}
                className={`${styles.link} ${pathname === href ? styles.active : ""}`}
            >
              <Link href={href}>
                <Image
                    src={src}
                    alt={alt}
                    width={20}
                    height={20}
                    className={pathname === href ? styles.activeSvg : ""}
                />
              </Link>
            </div>
        ))}
      </nav>
  );
};

export default Header;
