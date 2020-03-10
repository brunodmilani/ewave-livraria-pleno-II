export interface Livro {
  id: number;
  titulo: string;
  descricao: string;
  dataPublicacao: string;
  generoId: number;
  autorId: number;
  paginas: number;
  sinopse: string;
  capaPath: string;
  linkCompra: string;
}
