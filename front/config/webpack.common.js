const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

const dist = path.resolve(__dirname, '../dist');
const entry = path.resolve(__dirname, '../src/index.tsx');
const index = path.resolve(__dirname, '../src/index.html');
const postcss = path.resolve(__dirname, './styles');

const isProduction = process.env.NODE_ENV === 'production';
const API_HOST = process.env.API_HOST || 'http://95.163.251.29/api';
const CONTENT_HOST =
    process.env.CONTENT_HOST ||
    'https://protected-mountain-30162.herokuapp.com/images/fake';

module.exports = {
    entry,
    output: {
        filename: '[name].bundle.js',
        path: dist,
        publicPath: '/',
    },
    plugins: [
        new CleanWebpackPlugin(),
        new HtmlWebpackPlugin({
            title: 'spheridian',
            template: index,
        }),
        new webpack.EnvironmentPlugin({
            SERVER: false,
            API_HOST,
            CONTENT_HOST,
        }),
    ],
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    {
                        loader: 'style-loader',
                    },
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1,
                        },
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            config: {
                                path: postcss,
                            },
                        },
                    },
                ],
            },
            {
                test: /\.tsx?$/,
                use: [
                    {
                        loader: 'ts-loader',
                        options: {
                            transpileOnly: true,
                            experimentalWatchApi: true,
                        },
                    },
                ],
                exclude: /node_modules/,
            },
            {
                test: /\.(svg|png|gif|jpg|webp|eot|woff|ttf)$/,
                loader: 'file-loader',
            },
        ],
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },
};
