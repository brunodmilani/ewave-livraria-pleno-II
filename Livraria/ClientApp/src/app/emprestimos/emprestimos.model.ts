export interface Emprestimo {
  id: number;
  livroId: number;
  livro: string;
  usuarioId: number;
  usuario: string;
  data: string;
  dataDevolucao: string;
  diasRestantes: number;
  devolucao: boolean;
}

