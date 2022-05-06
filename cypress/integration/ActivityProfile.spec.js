describe('ActivityProfile', function () {

    // User enters in all inforomation 
    it('Correct Profile Information', function () {
        // Logs in 
        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('Matching').click()

        cy.contains('Activity Profile').click()

        // Making selections
        cy.get('input[type="checkbox"]')
            .check(['Studying', 'Exercising']);

        cy.get('input[type="radio"]')
            .check(['one']);

        // Saves
        cy.contains('Save').click()

    })

    // Profile Missing individual/group choice, error message should display
    it('Missing Activity choice', function () {
        cy.visit('https://localhost:5002/activityprofile')

        // Selections
        cy.get('input[type="radio"]')
            .check(['one']);

        // Doesnt save, error message shown
        cy.contains('Save').click()
    })

    // Profile missing requires/offers choice, error message shown when tries to save
    it('Missing schedule choice', function () {
        cy.visit('https://localhost:5002/activityprofile')

        // Selections
        cy.get('input[type="checkbox"]')
            .check(['Studying', 'Exercising']);

        // Doesnt save, error message shown
        cy.contains('Save').click()

    })

    // Clears selections user has made
    it('Clear Selections', function () {
        cy.visit('https://localhost:5002/activityprofile')

        // Selections
        cy.get('input[type="checkbox"]')
            .check(['Studying', 'Exercising']);

        cy.get('input[type="radio"]')
            .check(['one']);

        // Doesnt save, error message shown
        cy.contains('Clear Selections').click()

    })
})