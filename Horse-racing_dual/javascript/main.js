/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

const N = parseInt(readline());
const horses = [];
for (let i = 0; i < N; i++) {
  horses.push(parseInt(readline()));
}
horses.sort((a, b) => a - b);

let result = horses.reduce((acc, val) => acc < val ? val : acc);

for (let i = 1; i < N; i++) {
  const temp = horses[i] - horses[i - 1];
  if (temp < result) result = temp;
}

// Write an answer using console.log()
// To debug: console.error('Debug messages...');

console.log(result);