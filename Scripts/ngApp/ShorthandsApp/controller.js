(function () {
    angular.module("ShorthandsApp").controller("ShorthandsController", function ($resource) {
        var self = this;

        //Get published shorthands
        var Shorthand = $resource('api/shorthands/:id', null, {
            getPublishedShorthands: { method: 'GET', isArray: true, url: '/api/shorthands/getPublishedShorthands' }
        });

        self.getPublishedShorthands = function () {
            self.shorthands = Shorthand.getPublishedShorthands();
        };


        //Get all shorthands
        self.shorthands = Shorthand.query();

        //Create new shorthand
        self.post = function () { //post is function name
            var newShorthand = new Shorthand({
                title: this.Title,
                tags: this.Tags,
            });
            newShorthand.$save(function (result) {
                self.shorthands.push(result);
            });
        };
        //Update shorthand
        self.update = function (originalShorthand) {
            originalShorthand.Title.$save();
            originalShorthand.Tags.$save();
        };

        //Remove shorthand
        self.remove = function (originalShorthand) {
            originalShorthand.$remove({ id: originalShorthand.Id }, function () {
                self.shorthands = self.shorthands.filter(function (shorthand) {
                    return shorthand.Id != originalShorthand.Id;
                });
            });
        }
    });


})();