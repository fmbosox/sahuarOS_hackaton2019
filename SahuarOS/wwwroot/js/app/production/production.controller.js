(function (angular) {
    "use strict";

    angular.module('productionApp')
        .controller('ProductionController', ProductionController);

    ProductionController.$inject = [
        '$scope',
        '$http',
        'ProductionContext'];

    function ProductionController($scope, $http, context) {
        var presenter = ProductionPresenter();
        var self = {
            order: {},
            startProduct: startProduct,
            finishProduct: finishProduct
        };

        self.order = presenter.present(Order(context.order));


        function startProduct(productId, name, index) {
            var product = self.order.products[index];
            if (product.status.id === 0)
                $http({
                    url: '/Order/StartProduct',
                    method: "POST",
                    data: {orderId: self.order.id, productId: productId}
                }).then(function (response) {
                        product.status = presenter.presentStatus(1);
                        downloadURI('/Order/GCode/' + productId, name + '.gcode');
                    },
                    function (response) { // optional
                        // failed
                    });
            else
                downloadURI('/Order/GCode/' + productId, name + '.gcode');
        }


        function finishProduct(productId, name, index) {
            var product = self.order.products[index];
            $http({
                url: '/Order/FinishProduct',
                method: "POST",
                data: {orderId: self.order.id, productId: productId}
            }).then(function (response) {
                    product.status = presenter.presentStatus(2);
                },
                function (response) { // optional
                    // failed
                });

        }


        return self;
    }
})(angular);