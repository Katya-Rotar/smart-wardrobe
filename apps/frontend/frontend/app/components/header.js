"use client";

import Link from "next/link";
import Image from "next/image";
import { usePathname } from "next/navigation";
import styles from "../styles/header.module.css";

const Header = () => {
  const pathname = usePathname();

  const links = [
    { href: "/", src: "/home.svg", alt: "Home" },
    { href: "/wardrobe", src: "/clothing.svg", alt: "Clothing" },
    { href: "/outfit", src: "/wardrobe.svg", alt: "Wardrobe" },
  ];

  return (
    <nav className={styles.sidebar}>
      {links.map(({ href, src, alt }) => (
        <div
          key={href}
          className={`${styles.link} ${
            pathname === href ? styles.active : ""
          }`}
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
