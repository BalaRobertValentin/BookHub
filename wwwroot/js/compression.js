function compressText() {
    const inputText = document.getElementById("inputText").value;
    const compressed = lzwCompress(inputText);
    document.getElementById("outputText").value = compressed.join(",");
}

function decompressText() {
    const compressedText = document.getElementById("outputText").value.split(",").map(Number);
    const decompressed = lzwDecompress(compressedText);
    document.getElementById("outputText").value = decompressed;
}

function lzwCompress(input) {
    const dict = new Map();
    let currentCharCode = 256;
    let current = "";
    const result = [];

    for (const char of input) {
        const temp = current + char;
        if (dict.has(temp)) {
            current = temp;
        } else {
            const code = current.length === 1 ? current.charCodeAt(0) : dict.get(current);
            result.push(code);
            dict.set(temp, currentCharCode++);
            current = char;
        }
    }

    if (current.length > 0) {
        const code = current.length === 1 ? current.charCodeAt(0) : dict.get(current);
        result.push(code);
    }

    return result;
}

function lzwDecompress(compressed) {
    const dict = new Map();
    let currentCharCode = 256;

    let previous = String.fromCharCode(compressed[0]);
    let result = previous;

    for (let i = 1; i < compressed.length; i++) {
        const code = compressed[i];
        const current = code < currentCharCode ? (code < 256 ? String.fromCharCode(code) : dict.get(code)) : previous + previous[0];

        result += current;
        dict.set(currentCharCode++, previous + current[0]);
        previous = current;
    }

    return result;
}