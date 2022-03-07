package repository;

import model.Identifiable;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;

public class AbstractRepository<T extends Identifiable<id>,id> implements Repository<T,id> {
    protected Map<id,T> elements;

    public AbstractRepository() {
        elements = new HashMap<>();
    }

    @Override
    public void add(T elem) {
        if(elements.containsKey(elem.getId())){
            throw new RuntimeException("Element already exists!");
        }
        else
            elements.put(elem.getId(),elem);
    }

    @Override
    public void delete(T elem) {
        if (elements.containsKey(elem.getId())){
            elements.remove(elem.getId());
        }
    }

    @Override
    public void update(T elem, id id) {
        if(elements.containsKey(id))
            elements.put(elem.getId(),elem);
        else
            throw new RuntimeException("Element doesn't exist!");
    }

    @Override
    public T findById(id id) {
        if(elements.containsKey(id))
            return elements.get(id);
        else
            throw new RuntimeException("Elemeny doesn't exist!");
    }

    @Override
    public Iterable<T> findAll() {
        return elements.values();
    }
}
