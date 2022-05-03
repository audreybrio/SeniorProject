
describe('TutoringProfile', function () {

    // User enters in all inforomation 
    it('Correct Profile Information', function () {
        // Logs in 
        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('Matching').click()

        cy.contains('Tutoring Profile').click()

        // Making selections
        cy.get('input[type="radio"]')
            .check(['group']);

        cy.get('input[type="radio"]')
            .check(['offers']);

        cy.get('input[id="course1"]').type('CECS 475')
        cy.get('input[id="course2"]').type('CECS 328')

        cy.get('input[type="radio"]')
            .check(['one']);

        // Saves
        cy.contains('Save').click()

    })

    // Profile Missing individual/group choice, error message should display
    it('Missing Individual/Group choice', function () {
        cy.visit('https://localhost:5002/tutoringprofile')

        // Selections
        cy.get('input[type="radio"]')
            .check(['requires']);

        cy.get('input[id="course1"]').type('CECS 475')
        cy.get('input[id="course2"]').type('CECS 328')


        cy.get('input[type="radio"]')
            .check(['one']);

        // Doesnt save, error message shown
        cy.contains('Save').click()
    })

    // Profile missing requires/offers choice, error message shown when tries to save
    it('Missing requires/offers choice', function () {
        cy.visit('https://localhost:5002/tutoringprofile')

        // Selections
        cy.get('input[type="radio"]')
            .check(['group']);

        cy.get('input[id="course1"]').type('CECS 475')
        cy.get('input[id="course2"]').type('CECS 328')

        cy.get('input[type="radio"]')
            .check(['one']);

        // Doesnt save, error message shown
        cy.contains('Save').click()

    })

    // Profile Missing course information, error message shown when tries to save
    it('Missing courses', function () {
        cy.visit('https://localhost:5002/tutoringprofile')

        // Selections
        cy.get('input[type="radio"]')
            .check(['group']);

        cy.get('input[type="radio"]')
            .check(['requires']);


        cy.get('input[type="radio"]')
            .check(['one']);

        // Doesnt save, error message shown
        cy.contains('Save').click()


    })

    // Profile missing schedule selection, error message shown when tries to save
    it('Missing Schedule', function () {
        cy.visit('https://localhost:5002/tutoringprofile')

        // Selections 
        cy.get('input[type="radio"]')
            .check(['group']);

        cy.get('input[type="radio"]')
            .check(['requires']);

        cy.get('input[id="course1"]').type('CECS 475')
        cy.get('input[id="course2"]').type('CECS 328')

        // Doesnt save, error message shown
        cy.contains('Save').click()

 


    })
})
