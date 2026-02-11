import Image from "next/image";
import Link from "next/link";
import "@/app/styles/wardrobe/itemCard.css"; 

export default function ItemCard({ id, name, image }) {
  return (
    <Link href={`/wardrobe/${id}`} className="item-card">
      <div className="item-content">
        <Image 
          src={image} 
          alt={name} 
          width={180} 
          height={180} 
          className="item-image"
        />
        <div className="item-name">{name}</div>
      </div>
    </Link>
  );
}
