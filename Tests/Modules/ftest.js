function f({ a, b }) {
    console.log(arguments);
    console.log(a);
    console.log(b);
}

f({ a: 1, b: 2 });