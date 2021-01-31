export interface Image {
  id: number
  title: string;
  description: string;
  imageUrl: string;
  tags: Tag[];
}

export interface Tag {
  id: number;
  name: string;
  images: Image[];
}
