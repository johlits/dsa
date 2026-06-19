public class Solution {
    public bool IsValidSudoku(char[][] board) {

        // cols
        for (var x = 0; x < 9; x++) {
            bool[] arr = new bool[9];
            for (var y = 0; y < 9; y++) {
                var val = board[x][y];
                if (val != '.') {
                    if (arr[val - '1'] == true) {
                        return false;
                    }
                    else {
                        arr[val - '1'] = true;
                    }
                }
            }
        }

        // rows
        for (var y = 0; y < 9; y++) {
            bool[] arr = new bool[9];
            for (var x = 0; x < 9; x++) {
                var val = board[x][y];
                if (val != '.') {
                    if (arr[val - '1'] == true) {
                        return false;
                    }
                    else {
                        arr[val - '1'] = true;
                    }
                }
            }
        }

        // boxes
        for (var bx = 0; bx < 3; bx++) {
            for (var by = 0; by < 3; by++) {
                bool[] arr = new bool[9];
                for (var x = bx * 3; x < bx * 3 + 3; x++) {
                    for (var y = by * 3; y < by * 3 + 3; y++) {
                        var val = board[x][y];
                        if (val != '.') {
                            if (arr[val - '1'] == true) {
                                return false;
                            }
                            else {
                                arr[val - '1'] = true;
                            }
                        }
                    }
                }
            }
        }

        return true;

    }
}