let fs = require('fs');

let main = async () => {
    try {
        let separator = "// >>>>>>>";
        let devJs = fs.readFileSync('filmow-dev.user.js').toString().split(separator)[0];
        let dist = fs.readFileSync('./dist/app.js').toString();

        fs.writeFileSync('filmow-dev.user.js', devJs + separator + "\n\n" + dist);

        console.log("Ok");
    } catch (err) {
        console.error("Error:", err);
    }
}

main().then();
