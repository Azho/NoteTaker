(function () {
    angular.module('NoteTaker').controller('KeybindController', function () {
        

        var self = this;
        var Keybind = $resource('/api/keybinds/:id');
        self.keybinds = Keybind.query();

    });
})();