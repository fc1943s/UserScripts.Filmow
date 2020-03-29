let CONFIG = {
    fsharpEntry: './TestProject.fsproj',
    outputDir: './dist',
    babel: {
    //     presets: [
    //         ["@babel/preset-env", {
    //             "targets": {"node": "12"},
    //             "modules": false,
    //             "useBuiltIns": "usage",
    //             "corejs": 3,
    //         }],
    //     ],
    }
};

let path = require("path");
// let webpack = require("webpack");
// let HtmlWebpackPlugin = require('html-webpack-plugin');
// let CopyWebpackPlugin = require('copy-webpack-plugin');
// let MiniCssExtractPlugin = require("mini-css-extract-plugin");
// let ReplaceInFileWebpackPlugin = require('replace-in-file-webpack-plugin');

module.exports = {
    entry: {
        app: [CONFIG.fsharpEntry]
    },
    output: {
        path: path.join(__dirname, CONFIG.outputDir),
        filename: '[name].js'
    },
    mode: "production",
    // devtool: "eval-source-map",
    // optimization: {
    //     splitChunks: {
    //         chunks: "all"
    //     }
    // },
    plugins: [
        // new MiniCssExtractPlugin({ filename: 'style.css' }),
        // new CopyWebpackPlugin([{ from: CONFIG.assetsDir }]),
    ],
    resolve: {
        symlinks: false
    },
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: {
                    loader: "fable-loader",
                    options: {
                        babel: CONFIG.babel
                    }
                }
            },
            {
                test: /\.[tj]sx?$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: CONFIG.babel
                },
            },
            {
                test: /\.(sass|scss|css)$/,
                use: [
                    /*isProduction ? MiniCssExtractPlugin.loader : */'style-loader',
                    'css-loader',
                    'sass-loader',
                ],
            },
            // {
            //     test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|ttf|eot)(\?.*)?$/,
            //     use: ["file-loader"]
            // }
        ]
    }
};
