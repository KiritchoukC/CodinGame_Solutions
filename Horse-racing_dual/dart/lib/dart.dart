import 'dart:math';
import 'dart:io';

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
void main() {
  var N = int.parse(stdin.readLineSync());
  var horses = <int>[];
  for (var i = 0; i < N; i++) {
    horses.add(int.parse(stdin.readLineSync()));
  }

  horses.sort((a, b) => a - b);

  var result = horses.reduce((acc, val) => acc < val ? val : acc);

  for (var i = 1; i < N; i++) {
    var temp = horses[i] - horses[i - 1];
    if (temp < result) result = temp;
  }

  // Write an answer using print()
  // To debug: stderr.writeln('Debug messages...');

  print(result);
}
