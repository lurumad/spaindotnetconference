module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        bower: {
            install: {
                options: {
                    targetDir: './Scripts/Vendor',
                    layout: 'byComponent',
                    verbose: true,
                    cleanTargetDir: false
                }
            }
        },

        concat: {
            options: {
                separator: ';',
                stripBanners: true,
                banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' +
                  '<%= grunt.template.today("mm-dd-yyyy") %> */',
            },
            shoppingcart: {
                src: ['Features/ShoppingCart/Index.js'],
                dest: 'Scripts/Features/ShoppingCart.js'
            }
        },

        uglify: {
            options: {
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
            },
            shoppingcart: {
                src: 'Scripts/Features/ShoppingCart.js',
                dest: 'Scripts/Features/ShoppingCart.Index.min-<%= pkg.version %>.js'
            }
        }
    });

    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('install-bower-packages', ['bower']);
    grunt.registerTask('merge-js-files', ['concat']);
    grunt.registerTask('min-js-file', ['concat', 'uglify']);
    grunt.registerTask('default', ['bower', 'concat', 'uglify']);
}