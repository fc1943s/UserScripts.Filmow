let replace = require('replace-in-file');

let main = async () => {
    try {
        let results = await replace({
            files: process.argv[2],
            from: [ 
                /(version *)(\d{13}) *$/gm, 
                /(v=)(\d{13}) *$/gm 
            ],
            to: '$1' + new Date().getTime(),
        })
        console.log('Replacement results:', results);
    } catch (error) {
        console.error('Error occurred:', error);
    }
}

main().then();
