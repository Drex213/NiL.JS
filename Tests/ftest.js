function* gen() {
    yield* (new class {
        *method() {
            yield 1;
        }
    }).method();
}

console.log(...gen());