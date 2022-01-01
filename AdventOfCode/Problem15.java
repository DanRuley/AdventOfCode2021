package probs;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.PriorityQueue;

public class AdventOfCode {

	public static void main(String[] args) throws IOException {
		int[][] board = readLines();

		System.out.println(solve(board));
	}

	public static int[][] readLines() throws IOException {
		BufferedReader br = new BufferedReader(new FileReader("C://Users//drslc//Downloads//input.txt"));

		ArrayList<String> arr = new ArrayList<>();
		String line;
		while ((line = br.readLine()) != null) {
			arr.add(line);
		}

		int[][] board = new int[arr.size()][arr.get(0).length()];
		int[][] bigBoard = new int[board.length * 5][board[0].length * 5];

		for (int i = 0; i < board.length; i++) {
			for (int j = 0; j < board[0].length; j++) {
				board[i][j] = arr.get(i).charAt(j) - '0';
			}
		}

		for (int i = 0; i < bigBoard.length; i++) {
			int imul = i / board.length;
			for (int j = 0; j < bigBoard[0].length; j++) {
				int jmul = j / board[0].length;
				int boardi = i - imul * board.length;
				int boardj = j - jmul * board[0].length;
				bigBoard[i][j] = board[boardi][boardj] + imul + jmul;
				bigBoard[i][j] = bigBoard[i][j] >= 10 ? bigBoard[i][j] - 9 : bigBoard[i][j];
				//System.out.println(i + ", " + j + "  " + imul + ", " + jmul + "  " + boardi + ", " + boardj);
			}
		}

		return bigBoard;
	}

	private static ArrayList<Integer> neighbors(int n, int xlen, int ylen) {
		ArrayList<Integer> nb = new ArrayList<>();

		if (n % ylen > 0)
			nb.add(n - 1);
		if (n % ylen < ylen - 1)
			nb.add(n + 1);
		if (n / xlen > 0)
			nb.add(n - xlen);
		if (n / xlen < xlen - 1)
			nb.add(n + xlen);
		return nb;
	}

	public static int solve(int[][] board) {

		int xlen = board.length;
		int ylen = board[0].length;

		int[][] prev = new int[xlen][ylen];
		int[][] dist = new int[xlen][ylen];
		for (int i = 0; i < board.length; i++)
			for (int j = 0; j < board[0].length; j++) {
				prev[i][j] = -1;
				dist[i][j] = Integer.MAX_VALUE;
			}
		dist[0][0] = 0;

		PriorityQueue<Integer> pq = new PriorityQueue<>(new QCompare(dist));
		pq.add(0);

		int curr;
		while (pq.size() > 0) {
			curr = pq.poll();

			if (curr / xlen == xlen - 1 && curr % ylen == ylen - 1)
				break;

			for (int n : neighbors(curr, xlen, ylen)) {
				if (n == 0)
					continue;
				int alt = dist[curr / xlen][curr % ylen] + board[n / xlen][n % ylen];
				if (alt < dist[n / xlen][n % ylen]) {
					dist[n / xlen][n % ylen] = alt;
					prev[n / xlen][n % ylen] = curr;
					pq.add(n);
				}
			}
		}

		return dist[xlen - 1][ylen - 1];
	}

}

class QCompare implements Comparator<Integer> {

	int[][] dist;
	int xlen;
	int ylen;

	public QCompare(int[][] dist) {
		this.dist = dist;
		xlen = dist.length;
		ylen = dist[0].length;
	}

	@Override
	public int compare(Integer o1, Integer o2) {
		return dist[o1 / xlen][o1 % ylen] - dist[o2 / xlen][o2 % ylen];
	}
}
