(function() {

    var app = angular.module("LabExercise")
  
    var PersonController = function($scope) {
      $scope.message = "GitHub Viewer";
      $scope.username = "angular";
      $scope.repoSortOrder = "-stargazers_count";
    }
  
    app.controller("PersonController", ["$scope", PersonController]);
  
  }());