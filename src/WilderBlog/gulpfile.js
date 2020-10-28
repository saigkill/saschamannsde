/// <binding BeforeBuild='default' Clean='clean, minify, scripts' />

const { src, dest, series } = require('gulp');
const uglify = require('gulp-uglify');
const concat = require('gulp-concat');
const rimraf = require("rimraf");
const merge = require('merge2');

function minify() {
    var contact = src(["wwwroot/js/contact.js"])
        .pipe(uglify())
        .pipe(concat("contact.min.js"))
        .pipe(dest("wwwroot/lib/site"));

    var site = src(["wwwroot/js/*.js", '!wwwroot/js/tour*.js', '!wwwroot/js/contact.js'])
        .pipe(uglify())
        .pipe(concat("wilderblog.min.js"))
        .pipe(dest("wwwroot/lib/site"));

    return merge(site, contact);
}

// Dependency Dirs
var deps = {
    "jquery": {
        "dist/*": ""
    },
    "bootstrap": {
        "dist/**/*": ""
    },
    "cookieconsent": {
        "build/*": ""
    },
    "github-calendar": {
        "dist/*": ""
    },
    "highlightjs": {
        "*.js": "",
        "styles/*": "styles"
    },
    "lodash": {
        "lodash*.*": ""
    },
    "owl-carousel": {
        "owl-carousel/**": ""
    },
    "popper.js": {
        "dist/*": ""
    },
    "prismjs": {
        "components/*": ""
    },
    "respond.js": {
        "dest/*": ""
    },
    "slick-carousel": {
        "slick/*": ""
    },
    "tether": {
        "dist/**/*": ""
    },
    "typeit": {
        "dist/*": ""
    },
    "vue": {
        "dist/*": ""
    },
    "vee-validate": {
        "dist/*": ""
    },
    "vue-resource": {
        "dist/*": ""
    },
    "@fortawesome/fontawesome-free": {
        "**/*": ""
    }
};

function clean(cb) {
    return rimraf("wwwroot/lib/", cb);
}

function scripts() {

    let streams = [];

    for (var prop in deps) {
        console.log("Prepping Scripts for: " + prop);
        for (var itemProp in deps[prop]) {
            streams.push(src("node_modules/" + prop + "/" + itemProp)
                .pipe(dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
        }
    }

    return merge(streams);

}

exports.minify = minify;
exports.clean = clean;
exports.scripts = scripts;
exports.default = series(clean, minify, scripts);