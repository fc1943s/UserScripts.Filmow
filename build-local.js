let fs = require('fs');

let main = async () => {
    try {
        let separator = "// >>>>>>>";
        let localJs = fs.readFileSync('local.user.js').toString().split(separator)[0];
        let dist = fs.readFileSync('./dist/app.js').toString();
        
        fs.writeFileSync('local.user.js', localJs + separator + "\n\n" + dist);
        
        console.log("Ok");
    } catch (err) {
        console.error("Error:", err);
    }
}

main().then();
